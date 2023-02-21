using Telegram.Bot.Types;

namespace TelegramBotLib.Interfaces
{
    public interface IBaseHandler
    {
        Task ProcessUpdate(Update update);
    }
}
