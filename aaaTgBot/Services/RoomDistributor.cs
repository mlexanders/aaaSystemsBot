using aaaSystemsCommon.Models;
using aaaSystemsCommon.Services.CrudServices;
using User = aaaSystemsCommon.Models.User;
using aaaTgBot.Messages;
using Telegram.Bot.Types;
using TgBotLibrary;

namespace aaaTgBot.Services
{
    public class RoomHandler : IHandler
    {
        private readonly long chatId;
        private readonly RoomsService roomsService;
        private readonly UsersService userService;

        public RoomHandler(long chatId)
        {
            this.chatId = chatId;
            roomsService = TransientService.GetRoomsService();
            userService = TransientService.GetUsersService();
        }

        public async Task ProcessMessage(Message message)
        {
            if (await roomsService.GetByChatId(chatId) is null)
            {
                await CreateRoom(message);
            }
            else
            {
                await AddMessageRoom(chatId, message);
            }
        }

        private async Task<Room> CreateRoom(Message message)
        {
            var user = await userService.GetByChatId(chatId);

            return new Room()
            {
                ChatId = message.Chat.Id,
                ClientId = user.Id,
            };
        }

        public async Task AddMessageRoom(long chatId, Message message)
        {
            var notificate = Notificate(chatId, message);

            var roomMessagesService = TransientService.GetRoomMessagesService();

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
        }

        public async Task Notificate(long chatId, Message message)
        {
            var user = userService.GetByChatId(chatId);
            var admins = userService.Admins();

            await MassMailing.SendNotificateMessage((await admins).Select(u => u.ChatId).ToList(), await user, message.Text!);
        }
    }
}
