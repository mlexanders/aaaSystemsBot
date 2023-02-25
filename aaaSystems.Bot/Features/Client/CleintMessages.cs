namespace aaaSystems.Bot.Features.Client
{
    internal class ClientMessages : ExtendedMessages
    {
        internal ClientMessages(long chatId, int? callbackMessageId = null) : base(chatId, callbackMessageId) { }

        internal override async Task SendStartMessage()
        {
            buttonsGenerator.SetInlineButtons(ClientCallback.Good);
            await bot.SendMessage(ClientText.StartMessage, buttonsGenerator.GetButtons());
        }

        internal override async Task SendMenu()
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
