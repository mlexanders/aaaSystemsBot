using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotLibrary;

namespace aaaTgBot.Handlers
{
    public static class UpdateHandler
    {
        public static Dictionary<long, IHandler> BusyUsersIdAndService { get; set; } = new();

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            long chatId = default;
            if (update.Message is not null)
            {
                Console.WriteLine($"{update.Message.Text} : {update.Message.Chat.Id}");

                chatId = update.Message.Chat.Id;
                if (!BusyUsersIdAndService.ContainsKey(chatId)) await MainHandler.MessageProcessing(chatId, update.Message);
                if (BusyUsersIdAndService.ContainsKey(chatId)) BusyUsersIdAndService[chatId].ProcessMessage(update.Message);
            }
            else if (update.CallbackQuery is not null)
            {
                Console.WriteLine($"Callback: {update.CallbackQuery.Message.Text} : {update.CallbackQuery.Data}");

                chatId = update.CallbackQuery.Message != null ? update.CallbackQuery.Message.Chat.Id : throw new NotImplementedException();
                if (!BusyUsersIdAndService.ContainsKey(chatId)) await MainHandler.CallbackQueryProcessing(chatId, update.CallbackQuery);
                if (BusyUsersIdAndService.ContainsKey(chatId)) BusyUsersIdAndService[chatId].ProcessMessage(update.CallbackQuery.Message);
            }
            else if (update.MyChatMember is not null)
            {
                Console.WriteLine(chatId + " : " + update.MyChatMember.NewChatMember.Status);
            };
        }
    }
}
