using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Services.CrudServices;
using aaaTgBot.Data.Exceptions;
using aaaTgBot.Handlers;
using Telegram.Bot.Requests;

namespace aaaTgBot.Services
{
    public class SendersFactory
    {
        private readonly UsersService usersService;
        private readonly RoomHandler handler;
        private Dictionary<long, Sender> senders;
        public Dictionary<long, Sender> Senders { get { return senders; } }

        public SendersFactory(RoomHandler handler)
        {
            senders = new();
            usersService = TransientService.GetUsersService();
            this.handler = handler;
        }

        public async Task<Sender> GetSender(long chatId)
        {
            if (senders.ContainsKey(chatId)) return senders[chatId];

            var user = await usersService.Get(chatId) ?? throw new UserNotFound(chatId);

            if (user.Role is Role.Admin)
            {
                var admin = new Admin(user, handler);
                senders.Add(chatId, admin);
                return admin;
            }
            else if (user.Role is Role.User)
            {
                var client = new Client(user, handler);
                senders.Add(chatId, client);
                return client;
            }
            throw new UserNotFound(chatId);
        }
    }
}
