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
                "@" + AdminCallback.Requests => messages.ShowDialogs(),
                "@" + AdminCallback.Users => messages.ShowAllSenders(),

                _ => ProcessSpecialCallback(messages, callbackQuery),
            };
        }

        private Task ProcessSpecialCallback(AdminMessages messages, CallbackQuery callbackQuery)
        {
            if (string.IsNullOrWhiteSpace(callbackQuery.Data)) return Task.CompletedTask;
            var data = callbackQuery.Data;

            var id = Convert.ToInt64(string.Join("", data.Where(c => char.IsDigit(c))));

            if (data.Contains(AdminCallback.Write))
            {
                return messages.StartDialog(id);
            }

            if (data.Contains(AdminCallback.LoadDialog))
            {
                return messages.LoadDialog(id);
            }

            if (data.Contains(AdminCallback.MoreDetails))
            {
                return messages.LoadDialog(id);
            }

        }
    }
}
