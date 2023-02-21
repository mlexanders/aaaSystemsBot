using aaaSystems.Bot.Services;

namespace aaaSystems.Bot.Messages
{
    internal abstract class MessageCollector
    {
        protected BotService bot;

        public MessageCollector(long chatId)
        {
            bot = new BotService(chatId);
        }
    }
}
