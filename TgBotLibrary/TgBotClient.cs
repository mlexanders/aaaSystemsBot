using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBotLibrary
{
    public static class TgBotClient
    {
        public static TelegramBotClient BotClient { private set; get; } = null!;

        public static async Task StartBot(Func<ITelegramBotClient, Update, CancellationToken, Task> HandleUpdateAsync,
            Func<ITelegramBotClient, Exception, CancellationToken, Task> HandlePollingErrorAsync)
        {
            BotClient = new TelegramBotClient(BaseBotSettings.BotToken);

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            BotClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync ?? BaseHandlePollingErrorAsync,
                receiverOptions: receiverOptions
            );

            var me = await BotClient.GetMeAsync();
            LogService.LogStart(me.Username);

            while (true) { Thread.Sleep(int.MaxValue); }
        }

        private static Task BaseHandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            try
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };
                LogService.LogError(ErrorMessage);

            }
            catch (Exception e)
            {
                LogService.LogWarn(e.Message);
            }

            return Task.CompletedTask;
        }
    }
}