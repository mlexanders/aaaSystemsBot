using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace aaaTgBot.Handlers
{
    public static class Distributor
    {
        public static async Task Distribute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message.Chat.Id;
            if (update.Type == UpdateType.Message)
            {
                Console.WriteLine(update.Message.Text.ToString());
                await MainHandler.MessageProcessing(chatId, update.Message);
            };

            if (update.Type == UpdateType.CallbackQuery)
            {
                Console.WriteLine(update.CallbackQuery.Data);
                await MainHandler.CallbackQueryProcessing(message.chat.id, update.CallbackQuery);
            };


            //if (update.MyChatMember != null)  /// такое иногда бывает, почему хз
            //{
            //    Console.WriteLine(update.MyChatMember);
            //};

        }
    }
}
