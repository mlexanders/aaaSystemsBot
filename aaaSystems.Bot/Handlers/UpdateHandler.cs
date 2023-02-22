using aaaSystems.Bot.Data;
using aaaSystems.Bot.Services;
using Telegram.Bot.Types;
using TelegramBotLib.Interfaces;

namespace aaaSystems.Bot.Handlers
{
    internal class UpdateHandler : IBaseUpdateHandler
    {
        internal static Dictionary<long, IBaseSpecialHandler> HandlingSenders { get; set; } = new();

        public async Task HandleUpdateAsync(Update update)
        {
            if (HandlingSenders.TryGetValue(update.GetChatId(), out var specialHandler))
            {
                await specialHandler.ProcessUpdate(update);
            }
            else
            {
                await DistributorService.ProcessUpdate(update);
            }
        }
    }
}