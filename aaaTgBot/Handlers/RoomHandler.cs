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
        private readonly RoomMessagesService roomMessagesService;
        private Room CurrentRoom = null!;

        public RoomHandler(long chatId)
        {
            this.chatId = chatId;
            roomsService = TransientService.GetRoomsService();
            userService = TransientService.GetUsersService();
            roomMessagesService = TransientService.GetRoomMessagesService();
        }

        public async Task ProcessMessage(Message message)
        {
            try
            {
                await TryCheck(message.Chat.Id);
                if (message.From != null && message.From.IsBot) return; // кто то из админов откликнулся / кто то вошел в чат

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
                await (new MessageCollectorBase(message.Chat.Id).SendUnknownUserMessage());
            }
            catch (ParticipantNotFound e)
            {
                AddToRoom(e.ChatId);
            }
            catch (RoomNotFound)
            {
                await CreateRoom();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private async Task CreateRoom()
        {
            var user = await userService.GetByChatId(chatId);
            await roomsService.Post(new Room() { ChatId = chatId, ClientId = user.Id });
        }

        private async Task TryCheck(long id)
        {
            CurrentRoom = await GetCurrentRoom() ?? throw new RoomNotFound();
            var user = (await userService.GetByChatId(id)) ?? throw new UserNotFound(id);

            if (user.Role is Role.Admin) AddToRoom(id);
        }

        private async Task ForwardMessage(Message message)
        {
            var userT = userService.GetByChatId(message.Chat.Id);

            var busyId = new[] { chatId, message.Chat.Id };
            var recipientsId = UpdateHandler.BusyUsersIdAndService.Where(b => b.Value == this && !busyId.Contains(b.Key)).Select(b => b.Key).ToList();

            if ((await userT).Role is Role.Admin)
            {
                await SendMessageToClient(message);
            }
            else
            {
                await MassMailing.SendMessageToUsers(recipientsId, message.Text!);
            }
        }

        private void AddToRoom(long id)
        {
            if (!UpdateHandler.BusyUsersIdAndService.ContainsKey(id))
            {
                UpdateHandler.BusyUsersIdAndService.Add(id, this);
            }
        }

        private async Task RemoveFromRoom(long id)
        {
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
                var inDialog = UpdateHandler.BusyUsersIdAndService.Where(u => u.Key != chatId).Select(u => u.Key).ToList();

                var idsNotToBeRemoved = new HashSet<long>(inDialog);
                listIds?.RemoveAll(item => idsNotToBeRemoved.Contains(item));
            }
            finally
            {
                await MassMailing.SendNotificateMessage(listIds, await user, message.Text ?? throw new("Message is nullll"));
            }
        }

        private async Task AddMessage(Message message)
        {
            await roomMessagesService.Post(new RoomMessage()
            {
                MessageId = message.MessageId,
                RoomId = CurrentRoom.Id,
                ChatId = message.Chat.Id,
                Text = message.Text,
                From = message.From?.Username
            });
        }

        private async Task SendMessageToClient(Message message)
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
