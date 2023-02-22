using aaaSystemsCommon.Models;

namespace aaaSystemsCommon.Interfaces
{
    public interface ISender : ICrud<Sender, long>
    {
        Task<List<Sender>> Admins();
    }
}
