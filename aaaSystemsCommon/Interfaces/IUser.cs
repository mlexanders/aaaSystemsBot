using aaaSystemsCommon.Models;

namespace aaaSystemsCommon.Interfaces
{
    public interface IUser : ICrud<User, int>
    {
        Task<User> GetByChatId(long chatId);
    }
}
