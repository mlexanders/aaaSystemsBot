using aaaSystems.Bot.Handlers;

namespace aaaSystems.Bot.Data.Interfaces
{
    public interface IParticipantFactory
    {
        Task<Participant> Get(long chatId);
        Task<Client> GetClient(long chatId);
    }
}