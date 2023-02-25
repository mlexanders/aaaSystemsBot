using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Features.Client;
using Telegram.Bot.Types;
using TelegramBotLib.Handlers;

namespace aaaSystems.Bot.Handlers
{
    internal class ClientHandler : BaseHandler
    {
        protected override Task ProcessMessage(Message message)
        {
            var messages = new ClientMessages(message.Chat.Id);

            return message.Text switch
            {
                "/start" => messages.SendStartMessage(),
                _ => messages.SendUnknownMessage()
            };
        }

        protected override Task ProcessCallbackQuery(CallbackQuery callbackQuery)
        {
            var messages = new ClientMessages(callbackQuery.Message!.Chat.Id, callbackQuery.Message!.MessageId);

            return callbackQuery.Data switch
            {
                "@/start" => messages.SendStartMessage(),
                "@" + ClientCallback.Good => messages.SendMenu(),
                "@" + CommonCallback.Menu => messages.SendMenu(),
                "@" + ClientCallback.Write => messages.StartDialog(),
                _ => throw new NotImplementedException()
            };
        }
    }
}
