using aaaTgBot.Data;
using aaaTgBot.Messages;
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
                "@" + InlineButtonsTexts.Write => messageCollector.JoinToRoom(callbackQuery.Message, callbackQuery.Message.Chat.Id),
                "@" + InlineButtonsTexts.Rooms => messageCollector.SendRoomList(),
                _ => SpecialProcessing(callbackQuery, messageCollector)
            };
            await response;
        }

        private static Task SpecialProcessing(CallbackQuery callbackQuery, MessageCollector mc)
        {
            var data = callbackQuery.Data;
            if (string.IsNullOrWhiteSpace(data)) return Task.CompletedTask;

            if (data.Contains("SendMessagesRoom"))
            {
                var clientChatId = Convert.ToInt64(string.Join("", data.Where(c => char.IsDigit(c))));
                return mc.SendMessagesRoom(callbackQuery.Message.Chat.Id, clientChatId); //TODO
            }
            else if (data.Contains("JoinToRoom"))
            {
                var clientChatId = Convert.ToInt64(string.Join("", data.Where(c => char.IsDigit(c))));
                return mc.JoinToRoom(callbackQuery.Message, clientChatId); //TODO
            }
            else
            {
                return Task.CompletedTask;
            }
        }
    }
}
