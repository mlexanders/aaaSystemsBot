using aaaSystemsCommon.Models;

namespace aaaSystemsCommon.Interfaces
{
    public interface IUser : ICrud<User, long>
    {
        Task<List<User>> Admins();
    }
}
