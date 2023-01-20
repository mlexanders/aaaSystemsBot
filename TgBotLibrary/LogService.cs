using Telegram.Bot.Types;
using TgBotLibrary;

namespace aaaTgBot.Services
{
    public static class LogService
    {
        public static void LogInfo(string info) => BaseLog(LogType.INFO, info);
        public static void LogWarn(string warn) => BaseLog(LogType.WARN, warn);
        public static void LogError(string error) => BaseLog(LogType.ERROR, error);

        public static void LogServerNotFound(string actionName)
        {
            LogError($"Server not found (404). {actionName} - Not completed");
        }

        public static void LogStart(string username)
        {
            Console.Write($"{DateTime.Now} ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"BOT START : {username}");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("-----------------------------------------------------" +
                $"\nToken: {BaseBotSettings.BotToken}");

            try { Console.WriteLine($"BackEnd Root: {BaseBotSettings.BackRoot}"); } catch { /*don't send root*/}

            Console.WriteLine("-----------------------------------------------------\n");
        }

        public static void LogUpdate(Update update)
        {
            if (update.Message != null)
            {
                LogInfo($"ChatId: {update.Message.Chat.Id} | Message: {update.Message.Text}");
            }
            else if (update.CallbackQuery != null)
            {
                LogInfo($"ChatId: {update.CallbackQuery.Id} | Callback: {update.CallbackQuery.Data}");
            }
            else if (update.MyChatMember != null)
            {
                LogInfo($"ChatId - {update.MyChatMember.NewChatMember.Status}:" +
                        $" {update.MyChatMember.Chat.Id} | UserName: {update.MyChatMember.From.Username}");
            }
        }

        private static void BaseLog(LogType logType, string message)
        {
            Console.Write(DateTime.Now);
            Console.ForegroundColor = logType.GetCollor();
            Console.Write($" {logType}");
            Console.ResetColor();
            Console.WriteLine($" --- {message}");
        }

        private enum LogType
        {
            INFO,
            WARN,
            ERROR
        }

        private static ConsoleColor GetCollor(this LogType logType)
        {
            return logType switch
            {
                LogType.INFO => ConsoleColor.Green,
                LogType.WARN => ConsoleColor.DarkYellow,
                LogType.ERROR => ConsoleColor.Red,
                _ => ConsoleColor.White
            };
        }
    }
}
