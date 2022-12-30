using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace aaaTgBot.Handlers
{
    public static class Distributor
    {
        public static async Task Distribute(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var message = update.Message ?? throw new NotImplementedException();

            if (update.Message.Type == MessageType.Text)
            {
                await MainHandler.MessageProcessing(message.Chat.Id, message);
            };
        }
    }
}
