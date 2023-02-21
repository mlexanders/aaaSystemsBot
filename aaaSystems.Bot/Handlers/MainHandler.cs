using Telegram.Bot.Types;
using TelegramBotLib.Handlers;

namespace aaaSystems.Bot.Handlers
{
    internal class MainHandler : BaseHandler
    {
        public override async Task ProcessUpdate(Update update)
        {
            await base.ProcessUpdate(update);
        }

        public override Task ProcessMessage(Message message)
        {
            Task action = message.Text switch
            {
                "/start" => new Task(() => Console.WriteLine()),
                _ => throw new NotImplementedException()
            };
            return action;
        }

        public override Task ProcessCallbackQuery(CallbackQuery callbackQuery)
        {
            Task action = callbackQuery.Data switch
            {
                "/start" => new Task(() => Console.WriteLine()),
                _ => throw new NotImplementedException()
            };
            return action;
        }
    }
}
