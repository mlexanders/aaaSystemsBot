using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotLib.Interfaces;

namespace TelegramBotLib.Handlers
{
    public abstract class BaseHandler : IAdvancedHandler
    {
        public virtual Task ProcessUpdate(Update update)
        {
            Task process = update.Type switch
            {
                UpdateType.Message => ProcessMessage(update.Message!),
                UpdateType.CallbackQuery => ProcessCallbackQuery(update.CallbackQuery!),
                _ => throw new NotImplementedException("Update type not processed")
            };
            return process;
        }

        public abstract Task ProcessMessage(Message message);

        public abstract Task ProcessCallbackQuery(CallbackQuery callbackQuery);
    }
}
