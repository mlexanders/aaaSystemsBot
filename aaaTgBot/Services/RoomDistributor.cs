using aaaSystemsCommon.Models;
using aaaTgBot.Handlers;
using aaaTgBot.Messages;
using Telegram.Bot.Types;
using TgBotLibrary;

namespace aaaTgBot.Services
{
    public class RoomHandler : IHandler
    {
        private readonly long chatId;

        public RoomHandler(long chatId)
        {
            this.chatId = chatId;
            //UpdateHandler.BusyUsersIdAndService.Add(chatId, this);
        }

        public async Task ProcessMessage(Message message)
        {
            var roomsService = TransientService.GetRoomsService();
            var userService = TransientService.GetUsersService();
            
            if (await roomsService.GetByChatId(chatId) is null)
            {
                var user = await userService.GetByChatId(chatId);

                var room = new Room()
                {
                    ChatId = message.Chat.Id,
                    ClientId = user.Id,
                    RoomMessages = new List<RoomMessage>()
                    {
                        new RoomMessage()
                        {
                            DateTime = DateTime.Now,
                            MessageId = message.MessageId,
                            UserId = user.Id,
                        }
                    },
                    Participants = new()
                };

                await roomsService.Post(room);
            }
            else
            {
                await AddMessageRoom(chatId, message);
            }

            //return await roomsService.GetByChatId(chatId);
        }

        public static async Task<Room> AddMessageRoom(long chatId, Message message)
        {
            var roomsService = TransientService.GetRoomsService();
            var roomMessagesService = TransientService.GetRoomMessagesService();
            var userService = TransientService.GetUsersService();

            var notificate = Notificate(chatId, message);

            var room = await roomsService.GetByChatId(chatId);
            if (room is not null)
            {
                var user = await userService.GetByChatId(chatId);

                var roomMsg = new RoomMessage()
                {
                    DateTime = DateTime.Now,
                    MessageId = message.MessageId,
                    UserId = user.Id,
                    RoomId= room.Id
                };

                await roomMessagesService.Post(roomMsg);
            }

            await notificate;
            
            return await roomsService.GetByChatId(chatId);
        }

        public static async Task Notificate(long chatId, Message message)
        {
            var userService = TransientService.GetUsersService();

            var user = userService.GetByChatId(chatId);
            var admins = userService.Admins();

            //await MassMailing.SendNotificateMessage(a.Select(u => u.ChatId).ToList(), b, message.Text!);
            await MassMailing.SendNotificateMessage((await admins).Select(u => u.ChatId).ToList(), await user, message.Text!);
        }
    }
}
