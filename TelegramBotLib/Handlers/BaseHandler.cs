using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotLib.Interfaces;

namespace TelegramBotLib.Handlers
{
    public abstract class BaseHandler : IBaseHandler
    {
        public virtual Task ProcessUpdate(Update update)
        {
            return update.Type switch
            {
                UpdateType.Message => ProcessMessage(update.Message!),
                UpdateType.CallbackQuery => ProcessCallbackQuery(update.CallbackQuery!),
                _ => throw new NotImplementedException("Update type not processed")
            };
        }

        protected abstract Task ProcessMessage(Message message);

        protected abstract Task ProcessCallbackQuery(CallbackQuery callbackQuery);
    }
}
