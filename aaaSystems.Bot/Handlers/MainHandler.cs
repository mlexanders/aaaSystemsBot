using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Messages;
using Telegram.Bot.Types;
using TelegramBotLib.Handlers;

namespace aaaSystems.Bot.Handlers
{
    internal class MainHandler : BaseHandler
    {
        private UnauthorizedMessages messages;

        public override async Task ProcessUpdate(Update update)
        {
            await base.ProcessUpdate(update);
        }

        public override Task ProcessMessage(Message message)
        {
            messages = new UnauthorizedMessages(message.Chat.Id);

            Task action = message.Text switch
            {
                "/start" => messages.SendStartMessage(),
                _ => throw new NotImplementedException()
            };
            return action;
        }

        public override Task ProcessCallbackQuery(CallbackQuery callbackQuery)
        {
            messages = new UnauthorizedMessages(callbackQuery.Message!.Chat.Id);

            Task action = callbackQuery.Data switch
            {
                "@" + InlineButtonsTexts.Registration =>  messages.Registration(),
                _ => throw new NotImplementedException()
            };
            return action;
        }
    }
}
