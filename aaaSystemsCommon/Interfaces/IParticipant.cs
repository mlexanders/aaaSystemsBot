using aaaSystemsCommon.Models;

namespace aaaSystemsCommon.Interfaces
{
    public interface IParticipant : ICrud<Participant, int>
    {
        Task<Participant> GetByChatId(long chatId);
    }
}