using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Models;
using aaaTgBot.Data.Exceptions;
using aaaTgBot.Messages;
using aaaTgBot.Services;
using Telegram.Bot.Types;
using TgBotLibrary;
using User = aaaSystemsCommon.Models.User;
using aaaSystemsCommon.Services.CrudServices;

namespace aaaTgBot.Handlers
{
    public class RoomHandler : ISpecialHandler
    {
        private readonly RoomMessagesService roomMessagesService;
        private readonly SendersFactory sendersFactory;
        public readonly long clientChatId;
        private int? roomId;

        public RoomHandler(long clientChatId)
        {
            roomMessagesService = TransientService.GetRoomMessagesService();
            this.clientChatId = clientChatId;
            sendersFactory = new (this);
        }

        public async Task ProcessMessage(Message message)
        {
            try
            {
                var sender = await sendersFactory.GetSender(message.Chat.Id);

                await TryCheck(sender);
                if (message.From != null && message.From.IsBot) return;

                Task processing = message switch
                {
                    { Text: "/leave" } => sender.Remove(),
                    _ => sender.Update(message)
                };

                await processing;
                await SaveMessage(message);
                //notificateAdministrators????
            }
            catch (UserNotFound)
            {
                throw new NotImplementedException(); // goto registration
            }
            catch (RoomNotFound)
            {
                await CreateRoom(message);
            }
        }

        private Task TryCheck(Sender sender)
        {
            if (roomId == null) throw new RoomNotFound();
            if (sender.user.Role is Role.Admin) sender.Add();
            return Task.CompletedTask;
        }

        Task Notify()
        {
            throw new NotImplementedException();
        }

        #region OlderMethods

        private async Task CreateRoom(Message message) // TODO : 
        {
            var roomsService = TransientService.GetRoomsService();
            var room = await roomsService.GetByChatId(message.Chat.Id);

            if (room == null)
            {
                await roomsService.Post(new Room() { UserId = message.Chat.Id });
            }
            else
            {
                room = await roomsService.GetByChatId(message.Chat.Id);
                roomId = room.Id;
            }
            LogService.LogInfo($"Создана комната\n  chatId: {message.Chat.Id}\n roomId: {room.Id}");
        }


        private Task SaveMessage(Message message)
        {
            return roomMessagesService.Post(new RoomMessage()
            {
                Id = message.MessageId,
                UserId = message.Chat.Id,
                RoomId = roomId ?? throw new RoomNotFound(),
                Text = message.Text,
                From = message.From?.Username
            });
        }
        #endregion
    }
    //interface IObservable
    //}

    interface ISender
    {
        Task Update(Message message);
        Task Remove();
        void Add();
    }

    public abstract class Sender : ISender
    {
        protected readonly RoomHandler handler;
        public readonly User user;

        protected long ChatId { get; set; }

        protected Sender(User user, RoomHandler handler)
        {
            this.user = user;
            ChatId = user.Id;
            this.handler = handler;
        }

        public virtual void Add()
        {
            if (!UpdateHandler.BusyUsersIdAndService.ContainsKey(ChatId))
            {
                UpdateHandler.BusyUsersIdAndService.Add(ChatId, handler);
            }
        }

        public abstract Task Remove();

        public abstract Task Update(Message message);
    }

    public class Admin : Sender
    {
        public Admin(User user, RoomHandler handler) : base(user, handler) { }

        public override Task Remove()
        {
            if (UpdateHandler.BusyUsersIdAndService.ContainsKey(ChatId))
                UpdateHandler.BusyUsersIdAndService.Remove(ChatId);
            else throw new NotImplementedException("Сервис не найден");
            return Task.CompletedTask;
        }

        public override async Task Update(Message message)
        {
            await SendMessageToClient(message);
        }

        private async Task SendMessageToClient(Message message)
        {
            var mc = new MessageCollectorBase(handler.clientChatId);
            await mc.SendMessage(message.Text ?? "");
        }
    }

    public class Client : Sender
    {
        public Client(User user, RoomHandler handler) : base(user, handler) { }

        public override Task Remove()
        {
            var notificate = new List<Task>();
            foreach (var keyValue in UpdateHandler.BusyUsersIdAndService)
            {
                if (keyValue.Value.Equals(handler)) UpdateHandler.BusyUsersIdAndService.Remove(keyValue.Key);
                if (keyValue.Key != ChatId) notificate.Add(SendMessage(keyValue.Key, "Собеседник вышел из комнаты"));
            }
            Task.WaitAll(notificate.ToArray());
            return Task.CompletedTask; // TODO : fix is Task.WaitAll(notificate.ToArray());
        }

        public override async Task Update(Message message)
        {
            var busyId = new[] { handler.clientChatId, message.Chat.Id };
            var recipientsId = UpdateHandler.BusyUsersIdAndService.Where(b => b.Value == this && !busyId.Contains(b.Key)).Select(b => b.Key).ToList();

            await MassMailing.ForwardMessageToUsers(recipientsId, message);
        }

        private async Task SendMessage(long chatId, string text)
        {
            var mc = new MessageCollectorBase(chatId);
            await mc.SendMessage(text);
        }
    }
}
