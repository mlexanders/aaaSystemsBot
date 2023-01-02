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
        public async Task TryToStartRegistration()
        {
            ////UsersService usersService = SingletontService.GetUsersService();
            ////var user = await usersService.GetByChatId(chatId);
            //if (user == null)
            //{
            DistributionService.BusyUsersIdAndService.Add(chatId, new RegistrationHandler(chatId));
            //}
            //else
            //{
            //    await bot.SendMessage("Ты уже зареган");
            //}
        }


        //public Task SendStartMenu()
        //{
        //    //
        //}
    }
}