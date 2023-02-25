using aaaSystems.Bot.Data.Interfaces;
using aaaSystems.Bot.Handlers;
using aaaSystemsCommon.Difinitions;
using aaaSystemsCommon.Entity;

namespace aaaSystems.Bot.Services
{
    public class ParticipantFactory : IParticipantFactory
    {
        private Client client = null!;
        private readonly DialogHandler handler;

        public ParticipantFactory(DialogHandler handler)
        {
            this.handler = handler;
        }

        public async Task<Participant> Get(long chatId)
        {
            var sender = await GetSender(chatId);

            if (sender.Role is Role.Client)
            {
                client = new Client(sender, handler);
                return client;
            }
            else if (sender.Role is Role.Admin)
            {
                return new Admin(chatId, handler);
            }
            throw new NotImplementedException("Sender Role not defined");
        }

        public async Task<Client> GetClient(long chatId)
        {
            if (client == null)
            {
                var sender = await GetSender(chatId);
                if (sender.Role is Role.Client) client = new Client(sender, handler);
                else throw new NotImplementedException("Sender is not Client");
            }
            return client;
        }

        private static Task<Sender> GetSender(long chatId)
        {
            var senders = TransientService.GetSendersService();
            return senders.Get(chatId);
        }
    }
}