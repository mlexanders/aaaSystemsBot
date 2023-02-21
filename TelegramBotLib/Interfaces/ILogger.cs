using Telegram.Bot.Types;

namespace TelegramBotLib.Interfaces
{
    public interface ILogger
    {
        void LogInfo(string info);
        void LogWarn(string warn);
        void LogError(string error);
        void LogUpdate(Update update);
    }
}