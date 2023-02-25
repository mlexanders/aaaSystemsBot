using Telegram.Bot.Types;

namespace aaaSystems.Bot.Data.Interfaces
{
    public interface IDialogNotificationService
    {
        Task NotificateAdministrators(Message message);
    }
}
