using aaaTgBot.Services;

namespace aaaTgBot.Handlers
{
    public partial class MessageCollector // extended class for working with messageId
    {
        private readonly int messageId;

        public MessageCollector(long chatId, int messageId)
        {
            botService = new BotService(chatId);
            this.chatId = chatId;
            this.messageId = messageId;
        }

        //TODO : EditMessage

        public async Task DeleteMessage()
        {
            await botService.DeleteMessage(messageId);
        }
    }
}