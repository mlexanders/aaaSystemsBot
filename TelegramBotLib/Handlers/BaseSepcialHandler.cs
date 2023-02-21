using Telegram.Bot.Types;
using TelegramBotLib.Interfaces;

namespace TelegramBotLib.Handlers
{
    public abstract class BaseSepcialHandler : BaseHandler, IBaseSpecialHandler // TODO : refactoring and implementation
    {
        private Message? CurrentMessage;
        private int numberOfMessage = 0;
        private int numberOfReadings = 1;

        public void StartProcessing()
        {
            if (CurrentMessage == null) RegistrateProcessing();
        }

        public override async Task ProcessMessage(Message message)
        {
            CurrentMessage = message;
            numberOfMessage++;
        }

        public override Task ProcessUpdate(Update update)
        {
            return base.ProcessUpdate(update);
        }

        protected abstract Task RegistrateProcessing();

        protected Task<Message> GetMessage()
        {
            while (CurrentMessage == null || numberOfMessage != numberOfReadings) { }
            numberOfReadings++;
            return Task.FromResult(CurrentMessage);
        }
    }
}
