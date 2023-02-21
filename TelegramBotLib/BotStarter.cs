using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotLib.Interfaces;
using TelegramBotLib.Services;

namespace TelegramBotLib
{
    public class BotClient
    {
        public static TelegramBotClient Client { get; private set; } = null!;

        private readonly IBaseUpdateHandler updateHandler;

        public BotClient(IBaseUpdateHandler updateHandler, string botToken, string backRoot = null!)
        {
            this.updateHandler = updateHandler;
            BaseBotSettings.SetSettings(botToken, backRoot);
            Client = new TelegramBotClient(botToken);
        }

        public async Task Start()
        {
            var receiverOptions = new ReceiverOptions()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };

            Client.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions
            );

            var me = await Client.GetMeAsync();
            LogService.LogStart(me.Username!);

            var locker = new Task(() => LogService.LogInfo("End"));
            locker.Wait();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {
            try
            {
                LogService.LogUpdate(update);

                await updateHandler.HandleUpdateAsync(update);
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message);
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken _)
        {
            return Task.Run(() =>
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
            });
        }
    }
}
