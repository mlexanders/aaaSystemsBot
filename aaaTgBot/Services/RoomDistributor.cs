//using aaaSystemsCommon.Models;
//using aaaSystemsCommon.Services.CrudServices;
//using User = aaaSystemsCommon.Models.User;
//using aaaTgBot.Messages;
//using Telegram.Bot.Types;
//using TgBotLibrary;
//using aaaSystemsCommon.Models.Difinitions;
//using aaaTgBot.Handlers;

//namespace aaaTgBot.Services
//{
//    public class RoomHandler : IHandler
//    {
//        private readonly long chatId;
//        private readonly RoomsService roomsService;
//        private readonly UsersService userService;
//        private readonly ParticipantsService participantsService;

//        public RoomHandler(long chatId)
//        {
//            this.chatId = chatId;
//            roomsService = TransientService.GetRoomsService();
//            userService = TransientService.GetUsersService();
//            participantsService = TransientService.GetParticipantsService();
//        }

//        public async Task ProcessMessage(Message message)
//        {
//            if (message.Text != null && message.Text.Equals("/leave"))
//            {
//                await DeleteFromRoom(message);
//            }
//            else if (await roomsService.GetByChatId(chatId) is null)
//            {
//                await CreateRoom(message);
//            }
//            else
//            {
//                await AddMessageRoom(message);
//            }
//        }

//        private async Task DeleteFromRoom(Message message)
//        {
//            var user = await userService.GetByChatId(message.Chat.Id);
//            await participantsService.Delete(user.Id);

//            UpdateHandler.BusyUsersIdAndService.Remove(message.Chat.Id);
//        }

//        private async Task AddToRoom(Message message)
//        {
//            UpdateHandler.BusyUsersIdAndService.Add(message.Chat.Id, this);

//            var room = await roomsService.GetByChatId(chatId);

//            room.Participants.Add(new Participant()
//            {
//                RoomId = room.Id,
//                UserChatId = message.Chat.Id,
//            });

//            await roomsService.Patch(room);
//        }


//        private async Task CreateRoom(Message message)
//        {
//            var user = await userService.GetByChatId(chatId);

//            if (user.Role is Role.User)
//            {
//                await roomsService.Post(new Room()
//                {
//                    ChatId = message.Chat.Id,
//                    ClientId = user.Id,
//                });
//            }
//            else if (user.Role is Role.Admin)
//            {
//                await AddToRoom(message);
//            }
//        }

//        public async Task AddMessageRoom(Message message)
//        {
//            var user = await userService.GetByChatId(chatId);
//            var roomMessagesService = TransientService.GetRoomMessagesService();
            
//            var notificate = Notificate(message);

//            var room = await roomsService.GetByChatId(chatId);
//            if (!room.Participants.Select(p => p.UserChatId).Contains(message.Chat.Id))
//            {
//                await AddToRoom(message);
//                await participantsService.Post(new Participant()
//                {
//                    RoomId = room.Id,
//                    UserChatId = message.Chat.Id,
//                });
//            }
//            if (room is not null)
//            {
//                var roomMsg = new RoomMessage()
//                {
//                    DateTime = DateTime.Now,
//                    MessageId = message.MessageId,
//                    UserId = user.Id,
//                    RoomId= room.Id
//                };
//                await roomMessagesService.Post(roomMsg);
//            }
//            await notificate;
//        }

//        public async Task Notificate(Message message)
//        {
//            var senderChatId = message.Chat.Id;

//            if (chatId != senderChatId)
//            {
//                var messageCollector = new MessageCollectorBase(chatId);
//                Task response = message switch
//                {
//                    { Text : not null } =>  messageCollector.SendMessage(message.Text),
//                    _ => throw new("неизвестное сообщение")

//                };
//                await response;
//            }
//            else
//            {
//                var room = await roomsService.GetByChatId(chatId);
//                if (room.Participants is not null)
//                {
//                    var recipientsId = room.Participants.Where(p => p.UserChatId != chatId).Select(p => p.UserChatId).ToList();
//                    await MassMailing.SendMessages(recipientsId, message.Text);
//                }

//                var adminsChatId = await userService.Admins();
//                adminsChatId.RemoveAll(u => UpdateHandler.BusyUsersIdAndService.ContainsKey(u.ChatId));

//                await MassMailing.SendNotificateMessage(adminsChatId.Select(u => u.ChatId).ToList(), await userService.GetByChatId(chatId), message.Text!);
//            }
//        }
//    }
//}
