using aaaSystems.Bot.Features.Administrator;
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
                _ => messages.SendUnknownMessage()
            };
        }

        protected override Task ProcessCallbackQuery(CallbackQuery callbackQuery)
        {
            var messages = new AdminMessages(callbackQuery.Message!.Chat.Id, callbackQuery.Message.MessageId);

            return callbackQuery.Data switch
            {
                "@" + AdminCallback.Good => messages.SendMenu(),
                "@" + AdminCallback.Requests => messages.ShowRequests(),
                "@" + AdminCallback.Users => messages.ShowAllUsers(),

                _ => throw new NotImplementedException()
            };
        }
    }
}
