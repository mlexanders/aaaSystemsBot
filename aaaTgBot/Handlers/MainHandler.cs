using aaaTgBot.Data;
using aaaTgBot.Messages;
using Telegram.Bot.Types;

namespace aaaTgBot.Handlers
{
    public class MainHandler
    {
        public static async Task MessageProcessing(long chatId, Message message)
        {
            var messageCollector = new MessageCollector(chatId, message.MessageId);

            Task response = message.Text switch
            {
                "/start" => messageCollector.SendStartMessage(),
                _ => messageCollector.TryToStartRegistration(),
            };

            await response;
        }

        public static async Task CallbackQueryProcessing(long chatId, CallbackQuery callbackQuery)
        {
            var messageCollector = new MessageCollector(chatId, callbackQuery.Message.MessageId);

            Task response = callbackQuery.Data switch
            {
                "@" + InlineButtonsTexts.Forward => messageCollector.TryToStartRegistration(),
                "@" + InlineButtonsTexts.Write => messageCollector.SendInfoMessageAndGoToRoom(callbackQuery.Message), //TODO: не оченеь название...
                "@" + InlineButtonsTexts.Rooms => messageCollector.SendListRoom(),
                _ => SpecialProcessing(chatId, callbackQuery.Data, messageCollector)
            };;

            await response;
        }

        private static Task SpecialProcessing(long chatId, string? message, MessageCollector mc)
        {
            if (string.IsNullOrWhiteSpace(message)) return Task.CompletedTask;

            if (message.Contains($"GetRoom"))
            {
                var a = string.Join("", message.Where(c => char.IsDigit(c)));
                var chatIdClient = Convert.ToInt64(new string(a));
                return mc.GoToRoom(chatId, chatIdClient); //TODO
            }
            return Task.CompletedTask;
        }
    }
}
