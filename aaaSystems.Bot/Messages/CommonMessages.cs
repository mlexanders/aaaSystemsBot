using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Services;

namespace aaaSystems.Bot.Messages
{
    internal abstract class CommonMessages
    {
        protected readonly BotService bot;
        protected readonly long chatId;
        protected ButtonsGenerator buttonsGenerator;

        public CommonMessages(long chatId)
        {
            buttonsGenerator = new ();
            bot = new BotService(chatId);
            this.chatId = chatId;
        }

        public abstract Task SendStartMessage();

        public virtual async Task SendUnknownMessage()
        {
            buttonsGenerator.SetInlineButtons(InlineButtonsTexts.Menu);
            await bot.SendMessage(Texts.UnknownMessage, buttonsGenerator.GetButtons());
        }

        public abstract Task SendMenu();
    }
}
