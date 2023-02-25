using Telegram.Bot.Types;
using TelegramBotLib.Interfaces;

namespace TelegramBotLib.Handlers
{
    public abstract class BaseSepcialHandler : BaseHandler, IBaseSpecialHandler
    {
        private Message? CurrentMessage;
        private int numberOfMessage = 0;
        private int numberOfReadings = 1;

        public Task StartProcessing()
        {
            if (CurrentMessage == null) RegistrateProcessing();
            return Task.CompletedTask;
        }

        protected override async Task ProcessMessage(Message message)
        {
            CurrentMessage = message;
            numberOfMessage++;
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
