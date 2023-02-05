using aaaTgBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotLibrary;

namespace aaaTgBot.Handlers
{
    public static class UpdateHandler
    {
        public static Dictionary<long, ISpecialHandler> BusyUsersIdAndService { get; set; } = new();

        public static Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            LogService.LogUpdate(update);

            _ = Task.Run(() => Handle(update));
            return Task.CompletedTask;
        }

        private static async Task Handle(Update update)
        {
            try
            {
                if (update.Message is not null)
                {
                    var chatId = update.Message.Chat.Id;
                    if (!BusyUsersIdAndService.ContainsKey(chatId)) await MainHandler.MessageProcessing(chatId, update.Message);
                    if (BusyUsersIdAndService.ContainsKey(chatId)) await BusyUsersIdAndService[chatId].ProcessMessage(update.Message);
                }
                else if (update.CallbackQuery is not null)
                {
                    var chatId = update.CallbackQuery.Message != null ? update.CallbackQuery.Message.Chat.Id : throw new NotImplementedException();
                    if (!BusyUsersIdAndService.ContainsKey(chatId)) await MainHandler.CallbackQueryProcessing(chatId, update.CallbackQuery);
                    if (BusyUsersIdAndService.ContainsKey(chatId)) await BusyUsersIdAndService[chatId].ProcessMessage(update.CallbackQuery.Message);
                };
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message);
            }
        }
    }
}
