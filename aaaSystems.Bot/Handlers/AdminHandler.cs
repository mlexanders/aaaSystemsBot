using aaaSystems.Bot.Features.Administrator;
using aaaSystems.Bot.Messages;
using Telegram.Bot.Types;
using TelegramBotLib.Handlers;

namespace aaaSystems.Bot.Handlers
{
    internal class AdminHandler : BaseHandler
    {
        protected override Task ProcessMessage(Message message)
        {
            var messages = new AdminMessages(message!.Chat.Id);

            return message.Text switch
            {
                "/start" => messages.SendStartMessage(),
                _ => throw new NotImplementedException()
            };
        }

        protected override Task ProcessCallbackQuery(CallbackQuery callbackQuery)
        {
            var messages = new AdminMessages(callbackQuery.Message!.Chat.Id, callbackQuery.Message.MessageId);

            return callbackQuery.Data switch
            {
                "@" + AdminCallback.Good => messages.SendMenu(),
                "@" + AdminCallback.requests => messages.ShowRequests(),


                _ => throw new NotImplementedException()
            };
        }
    }
}
