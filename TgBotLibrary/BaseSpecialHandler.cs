using Telegram.Bot.Types;

namespace TgBotLibrary
{
    public abstract class BaseSepcialHandler : ISpecialHandler
    {
        private Message? CurrentMessage;
        private int numberOfMessage = 0;
        private int numberOfReadings = 1;

        protected void StartProcessing()
        {
            if (CurrentMessage == null) RegistrateProcessing();
        }

        public virtual async Task ProcessMessage(Message message)
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
