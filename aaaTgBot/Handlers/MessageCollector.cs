using aaaTgBot.Data;
using aaaTgBot.Services;

namespace aaaTgBot.Handlers
{
    public class MessageCollector
    {
        private readonly BotService botService;
        private readonly int messageId;

        public MessageCollector(long chatId, int messageId)
        {
            botService = new BotService(chatId);
            this.messageId = messageId;
        }

        public async Task SendStartMessage()
        {
            var buttonsGenerator = new ButtonsGenerator();
            buttonsGenerator.SetInlineButtons(InlineButtonsTexts.Forward);
            await botService.SendMessageAsync(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public async Task DeleteMessage()
        {
            await botService.DeleteMessageAsync(messageId);
        }

        //public Task SendStartMenu()
        //{
        //    //
        //}
    }
}