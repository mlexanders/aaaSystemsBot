using aaaTgBot.Data;
using Telegram.Bot.Types;

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
                _ => messageCollector.SendMessage(Texts.UnknownMessage)
            };

            await response;
        }

        public static async Task CallbackQueryProcessing(long chatId, CallbackQuery callbackQuery)
        {
            var messageCollector = new MessageCollector(chatId, callbackQuery.Message.MessageId);

            Task response = callbackQuery.Data switch
            {
                "@" + InlineButtonsTexts.Forward => messageCollector.TryToStartRegistration(),
                _ => messageCollector.SendMessage(Texts.UnknownMessage)
            };

            await response;
        }

        //private static Task AddMessageInChat(Message message)
        //{
        //    var chat = new Chat()
        //    {
                
        //    };
        //}
    }
}
