using Telegram.Bot.Types;

namespace aaaSystems.Bot.Data
{
    public static class Extentions
    {
        public static long GetChatId(this Update update)
        {
            if (update.Message != null)
            {
                return update.Message.Chat.Id;
            }
            else if (update.CallbackQuery != null && update.CallbackQuery.Message != null)
            {
                return update.CallbackQuery.Message.Chat.Id;
            }
            throw new NotImplementedException("The chatId wasn't found");
        }
    }
}