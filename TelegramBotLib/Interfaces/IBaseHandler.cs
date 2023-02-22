using Telegram.Bot.Types;

namespace TelegramBotLib.Interfaces
{
    public interface IProcessHandler
    {
        Task ProcessCallbackQuery(CallbackQuery callbackQuery);
        Task ProcessMessage(Message message);
    }
}