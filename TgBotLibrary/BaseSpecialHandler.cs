using Telegram.Bot.Types;

namespace TgBotLibrary
{
    public abstract class BaseSepcialHandlerNew : ISpecialHandler
    {
        private Task<Message>? currentMessage;

        protected Task StartProcessing()
        {
            return Task.Run(() =>
            {
                if (currentMessage == null) RegistrateProcessing().Start();
            });
        }

        public virtual async Task ProcessMessage(Message message)
        {
            currentMessage = new Task<Message>(() => { return message; });
            currentMessage.Start();
        }

        protected abstract Task RegistrateProcessing();

        protected async Task<Message> OnMessage()
        {
            while (currentMessage == null) { }
            currentMessage.Wait();

            var result = currentMessage.Result;
            currentMessage = null;
            return result;
        }
    }
}
