//using aaaSystems.Bot.Data.Exceptions;
//using aaaSystems.Bot.Services;
//using aaaSystemsCommon.Difinitions;
//using aaaSystemsCommon.Services.CrudServices;
//using Telegram.Bot;
//using Telegram.Bot.Types;
//using Telegram.Bot.Types.Enums;
//using TgBotLibrary;

//namespace aaaSystems.Bot.Handlers
//{
//    public class RoomHandlerOLD : ISpecialHandler
//    {
//        private readonly RoomMessagesService roomMessagesService;
//        //private readonly SendersFactory sendersFactory;
//        public readonly long clientChatId;
//        private int? roomId;

//        public RoomHandlerOLD(long clientChatId)
//        {
//            roomMessagesService = TransientService.GetRoomMessagesService();
//            this.clientChatId = clientChatId;
//            //sendersFactory = new(this);
//        }

//        public async Task ProcessMessage(Message message)
//        {
//            try
//            {
//                Sender sender = await sendersFactory.GetSender(message.Chat.Id);

//                if (roomId == null) throw new RoomNotFound();
//                if (sender.user.Role is Role.Admin) sender.Add();
//                if (message.From != null && message.From.IsBot) return;

//                Task processing = message switch
//                {
//                    { Text: "/leave" } => sender.Remove(),
//                    _ => sender.Update(message)
//                };

//                await processing;
//                await SaveMessage(message);
//                if (sender.user.Role == Role.Client) await Notify(message);
//            }
//            catch (UserNotFound)
//            {

//            }
//            catch (RoomNotFound)
//            {
//                await CreateRoom(message);
//            }
//        }

//        private async Task Notify(Message message)
//        {
//            var usersService = TransientService.GetSendersService();
//            var adminsIds = (await usersService.Admins()).Select(u => u.Id).Where(id => !UpdateHandler.BusyUsersIdAndService.ContainsKey(id)).ToList();
//            var client = await usersService.Get(clientChatId);

//            await MassMailing.SendNotificateMessage(adminsIds, client, message);
//        }

//        private async Task CreateRoom(Message message) // TODO : 
//        {
//            var roomsService = TransientService.GetRoomsService();
//            var room = await roomsService.GetByChatId(message.Chat.Id);

//            if (room == null)
//            {
//                await roomsService.Post(new Room() { UserId = message.Chat.Id });
//            }

//            room = await roomsService.GetByChatId(message.Chat.Id);
//            roomId = room.Id;
//            LogService.LogInfo($"Создана комната\n  chatId: {message.Chat.Id}\n roomId: {room.Id}");
//        }

//        private Task SaveMessage(Message message)
//        {
//            return roomMessagesService.Post(new RoomMessage()
//            {
//                Id = message.MessageId,
//                UserId = message.Chat.Id,
//                RoomId = roomId ?? throw new RoomNotFound(),
//                Text = message.Text,
//                From = message.From?.Username
//            });
//        }
//    }

//    public interface ISender
//    {
//        Task Update(Message message);
//        Task Remove();
//        void Add();
//    }

//    public abstract class Sender : ISender
//    {
//        protected readonly RoomHandlerOLD handler;
//        public readonly Sender user;

//        protected long ChatId { get; set; }

//        protected Sender(Sender user, RoomHandlerOLD handler)
//        {
//            this.user = user;
//            ChatId = user.Id;
//            this.handler = handler;
//        }

//        public virtual void Add()
//        {
//            if (!UpdateHandler.BusyUsersIdAndService.ContainsKey(ChatId))
//            {
//                UpdateHandler.BusyUsersIdAndService.Add(ChatId, handler);
//            }
//        }

//        public abstract Task Remove();

//        public abstract Task Update(Message message);
//    }

//    public class Admin : Sender
//    {
//        public Admin(Sender user, RoomHandlerOLD handler) : base(user, handler) { }

//        public override Task Remove()
//        {
//            if (UpdateHandler.BusyUsersIdAndService.ContainsKey(ChatId))
//            {
//                UpdateHandler.BusyUsersIdAndService.Remove(ChatId);
//                return Task.CompletedTask;
//            }
//            throw new NotImplementedException("Сервис не найден");
//        }

//        public override async Task Update(Message message)
//        {
//            await SendMessageToClient(message);
//        }

//        private async Task SendMessageToClient(Message message)
//        {
//            var mc = new MessageCollectorBase(handler.clientChatId);
//            var bot = TgBotClient.BotClient;

//            Task response = message.Type switch
//            {
//                MessageType.Photo => bot.SendPhotoAsync(handler.clientChatId, message.Photo.First().FileId, message.Caption),
//                MessageType.Text => mc.SendMessage(message.Text ?? ""),
//                MessageType.Voice => bot.SendVoiceAsync(handler.clientChatId, message.Voice.FileId),
//                _ => new Task(() => LogService.LogWarn("Необработанное сообщение"))

//            };
//            await response;
//        }
//    }

//    public class Client : Sender
//    {
//        public Client(Sender user, RoomHandlerOLD handler) : base(user, handler) { }

//        public override Task Remove()
//        {
//            var notificate = new List<Task>();
//            foreach (var keyValue in UpdateHandler.BusyUsersIdAndService)
//            {
//                if (keyValue.Value.Equals(handler)) UpdateHandler.BusyUsersIdAndService.Remove(keyValue.Key);
//                if (keyValue.Key != ChatId) notificate.Add(SendMessage(keyValue.Key, "Собеседник вышел из комнаты"));
//            }
//            Task.WaitAll(notificate.ToArray());
//            return Task.CompletedTask; // TODO : fix is Task.WaitAll(notificate.ToArray());
//        }

//        public override async Task Update(Message message)
//        {
//            var busyId = new[] { handler.clientChatId, message.Chat.Id };
//            var recipientsId = UpdateHandler.BusyUsersIdAndService.Where(b => b.Value == handler && !busyId.Contains(b.Key)).Select(b => b.Key).ToList();

//            await MassMailing.ForwardMessageToUsers(recipientsId, message);
//        }

//        private async Task SendMessage(long chatId, string text)
//        {
//            var mc = new MessageCollectorBase(chatId);
//            await mc.SendMessage(text);
//        }
//    }
//}
