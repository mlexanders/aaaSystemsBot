using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Features.Unauthorize;
using Telegram.Bot.Types;
using TelegramBotLib.Handlers;

namespace aaaSystems.Bot.Handlers
{
    internal class UnAuthorizedHandler : BaseHandler
    {
        protected override Task ProcessMessage(Message message)
        {
            var messages = new UnauthorizedMessages(message.Chat.Id);

            return message.Text switch
            {
                "/start" => messages.SendStartMessage(),
                _ => throw new NotImplementedException()
            };
        }

        protected override Task ProcessCallbackQuery(CallbackQuery callbackQuery)
        {
            var messages = new UnauthorizedMessages(callbackQuery.Message!.Chat.Id);

            return callbackQuery.Data switch
            {
                "@" + CommonCallback.Registration => messages.Registration(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
