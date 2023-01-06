namespace aaaTgBot.Handlers
{
    public class MessageCollector : MessageCollectorBase
    {
        private readonly int messageId;

        public MessageCollector(long chatId, int messageId) : base(chatId)
        {
            this.messageId = messageId;
        }

        //TODO : EditMessage

        public async Task DeleteMessage()
        {
            await botService.DeleteMessage(messageId);
        }
    }
}