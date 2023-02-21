using Telegram.Bot.Types;

namespace TelegramBotLib.Interfaces
{
    public interface IBaseUpdateHandler
    {
        Task HandleUpdateAsync(Update update);
    }
}
