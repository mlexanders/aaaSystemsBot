using aaaTgBot.Data;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotLibrary;

namespace aaaTgBot.Handlers
{
    public static class MainHandler
    {
        public static async Task MessageProcessing(long chatId, Message message)
        {
            var messageCollector = new MessageCollector(chatId, message.MessageId);

            Task response = message.Text switch
            {
                "/start" => messageCollector.SendStartMessage(),
                _ => NewMethod(TgBotClient.BotClient, chatId, message)
            };

            await response;
        }

        public static async Task CallbackQueryProcessing(int chatId, CallbackQuery callbackQuery)
        {
            var messageCollector = new MessageCollector(chatId, callbackQuery.Message.MessageId);

            Task response = ("@" + callbackQuery.Data) switch
            {
                InlineButtonsTexts.Forward => messageCollector.SendStartMenu(),
                _ => NewMethod(TgBotClient.BotClient, chatId, callbackQuery.Message)
            };

            await response;
        }

        private static async Task<Message> NewMethod(ITelegramBotClient botClient, long id, Message message)
        {
            return await botClient.SendTextMessageAsync(id, message.Text!);
        }
    }
}
