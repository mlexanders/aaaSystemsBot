using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBotLibrary
{
    public static class Bot
    {
        public static async Task StartBot(
            string botToken, Func<ITelegramBotClient, Update, CancellationToken, Task> HandleUpdateAsync,
            Func<ITelegramBotClient, Exception, CancellationToken, Task> HandlePollingErrorAsync)
        {
            var botClient = new TelegramBotClient(botToken);

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");

            while (true) { Thread.Sleep(int.MaxValue); }
        }
    }
}