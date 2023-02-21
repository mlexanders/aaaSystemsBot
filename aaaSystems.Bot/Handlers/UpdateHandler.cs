using aaaSystems.Bot.Data;
using Telegram.Bot.Types;
using TelegramBotLib.Interfaces;

namespace aaaSystems.Bot.Handlers
{
    internal class UpdateHandler : IBaseUpdateHandler
    {
        public static Dictionary<long, IBaseSpecialHandler> HandlingSenders { get; set; } = new();
        private static readonly IAdvancedHandler mainHandler = new MainHandler();

        public async Task HandleUpdateAsync(Update update)
        {
            if (HandlingSenders.TryGetValue(update.GetChatId(), out var specialHandler))
            {
                await specialHandler.ProcessUpdate(update);
            }
            else
            {
                await mainHandler.ProcessUpdate(update);
            }
        }
    }
}