using aaaSystemsCommon.Entity;

namespace aaaSystemsCommon.Interfaces
{
    public interface IRoom : ICrud<Room, int>
    {
        Task<Room> GetByChatId(long chatId);
    }
}