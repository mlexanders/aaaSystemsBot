using aaaSystemsCommon.Entity;

namespace aaaSystemsCommon.Interfaces
{
    public interface IDialog : ICrud<Dialog, long>
    {
        Task<Dialog> GetByChatId(long chatId);
    }
}
