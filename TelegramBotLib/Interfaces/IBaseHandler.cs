using Telegram.Bot.Types;

namespace TelegramBotLib.Interfaces
{
    public interface IAdvancedHandler : IBaseHandler
    {
        Task ProcessCallbackQuery(CallbackQuery callbackQuery);
        Task ProcessMessage(Message message);
    }
}