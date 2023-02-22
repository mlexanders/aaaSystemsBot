using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Services;
using TelegramBotLib.Interfaces;

namespace aaaSystems.Bot.Features
{
    internal abstract class CommonMessages
    {
        protected readonly long chatId;
        protected readonly IBaseBotService bot;
        protected ButtonsGenerator buttonsGenerator;

        public CommonMessages(long chatId)
        {
            buttonsGenerator = new();
            bot = new BotService(chatId);
            this.chatId = chatId;
        }

        public abstract Task SendStartMessage();

        public virtual async Task SendUnknownMessage()
        {
            buttonsGenerator.SetInlineButtons(CommonCallback.Menu);
            await bot.SendMessage(Texts.UnknownMessage, buttonsGenerator.GetButtons());
        }

        public abstract Task SendMenu();
    }
}
