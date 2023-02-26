using aaaSystemsCommon.Entity;

namespace aaaSystemsCommon.Interfaces
{
    public interface IDialogMessage : ICrud<DialogMessage, int>
    {
        Task PostByChatId(long chatId, int messageId);
        Task<List<DialogMessage>> GetByDialogId(long dialogId);
    }
}
