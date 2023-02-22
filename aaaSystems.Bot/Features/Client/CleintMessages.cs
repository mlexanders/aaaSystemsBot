namespace aaaSystems.Bot.Features.Client
{
    internal class ClientMessages : ExtendedMessages
    {
        public ClientMessages(long chatId, int? callbackMessageId = null) : base(chatId, callbackMessageId) { }

        public override async Task SendStartMessage()
        {
            buttonsGenerator.SetInlineButtons(ClientCallback.Good);
            await bot.SendMessage(ClientText.StartMessage, buttonsGenerator.GetButtons());
        }

        public override async Task SendMenu()
        {
            buttonsGenerator.SetInlineButtons(ClientCallback.Menu);
            await bot.SendMessage(ClientText.InfoMessage, buttonsGenerator.GetButtons());
        }

        internal Task AddToRoom()
        {
            throw new NotImplementedException();
        }
    }
}
