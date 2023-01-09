using aaaSystemsCommon.Models;
using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Services.CrudServices;
using aaaTgBot.Data.Exceptions;
using aaaTgBot.Messages;
using aaaTgBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotLibrary;

namespace aaaTgBot.Handlers
{
    public class RoomHandler : IHandler
    {
        private readonly long chatId;
        private readonly RoomsService roomsService;
        private readonly UsersService userService;
        private readonly ParticipantsService participantsService;
        private readonly RoomMessagesService roomMessagesService;
        private Room CurrentRoom = null!;

        public RoomHandler(long chatId)
        {
            this.chatId = chatId;
            roomsService = TransientService.GetRoomsService();
            userService = TransientService.GetUsersService();
            participantsService = TransientService.GetParticipantsService();
            roomMessagesService = TransientService.GetRoomMessagesService();
        }

        public async Task ProcessMessage(Message message)
        {
            if (message == null) return;
            if (message.From != null && message.From.IsBot) return; // кто то из админов откликнулся

            try
            {
                await TryCheck(message.Chat.Id);

                var notificate = NotificateOther(message);

                if (message.Type == MessageType.Text)
                {
                    Task processing = message.Text switch
                    {
                        "/leave" => RemoveFromRoom(message.Chat.Id),
                        _ => ForwardMessage(message)
                    };
                    await AddMessage(message);

                    await notificate;
                    await processing;
                }
            }
            catch (UserNotFound)
            {
                await (new MessageCollectorBase(message.Chat.Id).TryToStartRegistration());
            }
            catch (ParticipantNotFound e)
            {
                await AddToRoom(e.ChatId);
            }
            catch (RoomNotFound)
            {
                await CreateRoom();
            }
        }

        private async Task AddMessage(Message message)
        {
            await roomMessagesService.Post(new RoomMessage()
            {
                MessageId = message.MessageId,
                RoomId = CurrentRoom.Id,
                ChatId = message.Chat.Id,
                Text = message.Text ?? "null", //TODO : text
                From = message.From?.Username
            });
        }

        private async Task CreateRoom()
        {
            var user = await userService.GetByChatId(chatId);
            await roomsService.Post(new Room() { ChatId = chatId, ClientId = user.Id });
        }

        private async Task TryCheck(long id)
        {
            var userT = userService.GetByChatId(id);
            CurrentRoom = await GetCurrentRoom() ?? throw new RoomNotFound();

            if (await userT == null) throw new UserNotFound(id);
            if (CurrentRoom.Participants != null)
            {
                var p = CurrentRoom.Participants.Find(u => u.UserChatId == id);  //TODO : _
                if (id != chatId && p == null) throw new ParticipantNotFound(id);
            }
        }

        private async Task ForwardMessage(Message message)
        {
            CurrentRoom = await GetCurrentRoom();

            if (CurrentRoom.Participants != null && chatId == message.Chat.Id)
            {
                var participantsId = CurrentRoom.Participants.Select(p => p.UserChatId).ToList();
                await MassMailing.SendMessages(participantsId, message.Text!);
            }
            else
            {
                var user = await userService.GetByChatId(message.Chat.Id);
                if (user.Role is Role.Admin) await SendMessage(message);
            }
        }

        private async Task AddToRoom(long id)
        {
            if (!UpdateHandler.BusyUsersIdAndService.ContainsKey(id))
            {
                UpdateHandler.BusyUsersIdAndService.Add(id, this);
            }
            if (await participantsService.GetByChatId(id) == null)
            {
                await participantsService.Post(new Participant() { UserChatId = id, RoomId = CurrentRoom.Id });
            }
        }

        private async Task RemoveFromRoom(long id)
        {
            var user = await userService.GetByChatId(id);
            if (user == null) throw new UserNotFound(id);

            while (await participantsService.Get(user.Id) != null)
            {
                await participantsService.Delete(user.Id);
            }

            if (UpdateHandler.BusyUsersIdAndService.ContainsKey(id)) UpdateHandler.BusyUsersIdAndService.Remove(id);
            else throw new NotImplementedException("Сервис не найден");
            await SendMessage(id, "Вы вышли из чата, сообщения больше не будут доставлены");
        }

        private async Task NotificateOther(Message message)
        {
            var user = userService.GetByChatId(chatId);
            CurrentRoom = await GetCurrentRoom();
            var listIds = (await userService.Admins()).Select(a => a.ChatId).ToList();

            try
            {
                var inDialog = CurrentRoom.Participants ?? throw new();

                var idsNotToBeRemoved = new HashSet<long>(inDialog.Select(p => p.UserChatId));
                listIds?.RemoveAll(item => idsNotToBeRemoved.Contains(item));
            }
            finally
            {
                await MassMailing.SendNotificateMessage(listIds, await user, message.Text ?? throw new("Message is nullll"));
            }
        }

        private async Task SendMessage(Message message)
        {
            var mc = new MessageCollectorBase(chatId);
            await mc.SendMessage(message.Text ?? "пустая смс-ка : -)");
        }

        private async Task SendMessage(long id, string text)
        {
            var mc = new MessageCollectorBase(id);
            await mc.SendMessage(text ?? "пустая смс-ка : -)");
        }

        private Task<Room> GetCurrentRoom()
        {
            return roomsService.GetByChatId(chatId);
        }
    }
}
