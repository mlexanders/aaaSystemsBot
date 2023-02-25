using Telegram.Bot.Types;
using TelegramBotLib.Handlers;

namespace aaaSystems.Bot.Handlers
{
    internal class DialogHandler : BaseSepcialHandler
    {
        public DialogHandler()
        {

        }

        protected override Task ProcessMessage(Message message)
        {
            return base.ProcessMessage(message);
        }

        protected override Task ProcessCallbackQuery(CallbackQuery callbackQuery)
        {
            throw new NotImplementedException();
        }

        protected override Task RegistrateProcessing()
        {
            throw new NotImplementedException();
        }
    }
}
