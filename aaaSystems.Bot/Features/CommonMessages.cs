using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Services;
using TelegramBotLib.Interfaces;

namespace aaaSystems.Bot.Features
{
    internal abstract class CommonMessages
    {
        protected readonly long chatId;
        protected readonly BotService bot;
        protected ButtonsGenerator buttonsGenerator;

        internal CommonMessages(long chatId)
        {
            buttonsGenerator = new();
            bot = new (chatId);
            this.chatId = chatId;
        }

        internal abstract Task SendStartMessage();

        internal virtual async Task SendUnknownMessage()
        {
            buttonsGenerator.SetInlineButtons(CommonCallback.Menu);
            await bot.SendMessage(Texts.UnknownMessage, buttonsGenerator.GetButtons());
        }

        internal abstract Task SendMenu();
    }
}
