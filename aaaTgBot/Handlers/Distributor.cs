using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace aaaTgBot.Handlers
{
    public static class Distributor
    {
        public static async Task Distribute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message)
            {
                Console.WriteLine(update.Message.Text.ToString());
                await MainHandler.MessageProcessing(update.Message.Chat.Id, update.Message);
            };

            if (update.Type == UpdateType.CallbackQuery)
            {
                Console.WriteLine(update.CallbackQuery.Data);
                await MainHandler.CallbackQueryProcessing(update.CallbackQuery.Message.Chat.Id, update.CallbackQuery);
            };

            //if (update.MyChatMember != null)  /// такое иногда бывает, почему хз
            //{
            //    Console.WriteLine(update.MyChatMember);
            //};
        }
    }
}
