using aaaSystems.Bot.Data.Texts;

namespace aaaSystems.Bot.Messages
{
    internal class CleintMessages : CommonMessages
    {
        public CleintMessages(long chatId) : base(chatId) { }

        public override async Task SendStartMessage()
        {
            //buttonsGenerator.SetInlineButtons();
            await bot.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public override Task SendMenu()
        {
            return SendStartMessage();
        }
    }
}
