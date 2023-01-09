using aaaSystemsCommon.Models;

namespace aaaSystemsCommon.Interfaces
{
    public interface IRoom : ICrud<Room, int>
    {
        Task<Room> GetByChatId(long chatId);
    }
}