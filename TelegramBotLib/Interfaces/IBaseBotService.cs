using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBotLib.Interfaces
{
    public interface IBaseBotService
    {
        Task SendMessage(string message, IReplyMarkup button);
        Task DeleteMessage(int messageId);
        Task EditMessage(int messageId, string text, IReplyMarkup? markup = null);
    }
}
