using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Handlers;

namespace aaaSystems.Bot.Messages
{
    internal class UnauthorizedMessages : CommonMessages
    {
        public UnauthorizedMessages(long chatId) : base(chatId) { }

        public override async Task SendStartMessage()
        {
            buttonsGenerator.SetInlineButtons(InlineButtonsTexts.Registration);
            await bot.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public override Task SendMenu()
        {
            return SendStartMessage();
        }

        public async Task Registration() //TODO : add/remove from HandlingSenders
        {
            var specialHandler = new RegistrationHandler(chatId);
            UpdateHandler.HandlingSenders.Add(chatId, specialHandler);
            specialHandler.StartProcessing();
        }
    }
}
