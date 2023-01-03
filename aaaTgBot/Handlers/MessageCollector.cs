using aaaTgBot.Data;
using aaaTgBot.Services;
using Telegram.Bot.Types;

namespace aaaTgBot.Handlers
{
    public class MessageCollector
    {
        private readonly BotService botService;
        private readonly long chatId;
        private readonly int messageId;

        public MessageCollector(long chatId, int messageId)
        {
            botService = new BotService(chatId);
            this.chatId = chatId;
            this.messageId = messageId;
        }

        public async Task SendStartMessage()
        {
            var buttonsGenerator = new ButtonsGenerator();
            buttonsGenerator.SetInlineButtons(InlineButtonsTexts.Forward);
            await botService.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public async Task DeleteMessage()
        {
            await botService.DeleteMessage(messageId);
        }

        public async Task SendMessage(string text)
        {
            await botService.SendMessage(text);
        }

        public async Task TryToStartRegistration()
        {
            DistributionService.BusyUsersIdAndService.Add(chatId, new RegistrationHandler(chatId));
        }
    }
}