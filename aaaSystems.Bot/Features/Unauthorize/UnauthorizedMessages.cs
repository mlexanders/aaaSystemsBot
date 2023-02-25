using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Handlers;

namespace aaaSystems.Bot.Features.Unauthorize
{
    internal class UnauthorizedMessages : CommonMessages
    {
        public UnauthorizedMessages(long chatId) : base(chatId) { }

        internal override async Task SendStartMessage()
        {
            buttonsGenerator.SetInlineButtons(CommonCallback.Registration);
            await bot.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        internal override Task SendMenu()
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
