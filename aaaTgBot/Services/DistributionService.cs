using aaaTgBot.Handlers;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotLibrary;

namespace aaaTgBot.Services
{
    public static class DistributionService
    {
        public static Dictionary<long, BaseSpecialHandler> BusyUsersIdAndService { get; set; } = new();

        public static async Task Distribute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var chatId = update.Message is not null ? update.Message.Chat.Id : update.CallbackQuery is not null ? update.CallbackQuery.Message.Chat.Id : throw new NotImplementedException();

            if (update.Type == UpdateType.Message)
            {
                Console.WriteLine(update.Message.Type == MessageType.Text ? update.Message.Text.ToString() : update.Message.Type);
                
                if (!BusyUsersIdAndService.ContainsKey(chatId)) MainHandler.MessageProcessing(chatId, update.Message);
                if (BusyUsersIdAndService.ContainsKey(chatId)) BusyUsersIdAndService[chatId].ProcessMessage(update.Message);
            };

            if (update.Type == UpdateType.CallbackQuery)
            {
                Console.WriteLine(update.CallbackQuery.Data);
                if (!BusyUsersIdAndService.ContainsKey(chatId)) MainHandler.CallbackQueryProcessing(chatId, update.CallbackQuery);
                if (BusyUsersIdAndService.ContainsKey(chatId)) BusyUsersIdAndService[chatId].ProcessMessage(update.CallbackQuery.Message);
            };

            if (update.MyChatMember != null)
            {
                Console.WriteLine(chatId + " : " + update.MyChatMember.NewChatMember.Status);
            };
        }
    }
}
