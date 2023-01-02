using Telegram.Bot.Types.ReplyMarkups;

namespace TgBotLibrary
{
    public interface IBotService
    {
        Task SendMessage(string message, IReplyMarkup button);
    }
}