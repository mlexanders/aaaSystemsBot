using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotLib.Interfaces
{
    public interface IBaseBotService
    {
        Task SendMessage(string message);
        Task SendMessage(string message, IReplyMarkup button);
    }
}
