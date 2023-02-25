using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Utils;
using aaaTgBot.Data;
using aaaTgBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace aaaTgBot.Messages
{
    public class MessageCollectorBase
    {
        protected readonly BotService botService;
        protected readonly long chatId;

        public MessageCollectorBase(long chatId)
        {
            botService = new BotService(chatId);
            this.chatId = chatId;
        }

        public async Task SendStartMessage()
        {
            var buttonsGenerator = new ButtonsGenerator();
            buttonsGenerator.SetInlineButtons(InlineButtonsTexts.Registration);
            await botService.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public async Task SendMessage(string text)
        {
            await botService.SendMessage(text);
        }

        public async Task SendUnknownUserMessage()
        {
            var button = new ButtonsGenerator();
            button.SetInlineButtons((InlineButtonsTexts.Registration));
            await botService.SendMessage(Texts.UnknownUser, button.GetButtons());
        }

        public async Task SendUnknownMessage()
        {
            var button = new ButtonsGenerator();
            button.SetInlineButtons((InlineButtonsTexts.Menu));
            await botService.SendMessage(Texts.UnknownMessage, button.GetButtons());
        }

        public async Task SendMenu()
        {
            await SendStartMessage();
        }
        public async Task SendActionMenu()
        {
            var user = await TransientService.GetUsersService().Get(chatId);
            var bg = new ButtonsGenerator();

            if (user.Role is Role.User)
            {
                bg.SetInlineButtons(InlineButtonsTexts.Write);
                bg.SetInlineButtons(InlineButtonsTexts.SendApplication);
                await botService.SendMessage(Texts.SubmitAnApplication, bg.GetButtons());
            }
            else if (user.Role is Role.Admin)
            {
                bg.SetInlineButtons(InlineButtonsTexts.Rooms);
                await botService.SendMessage("Что бы вы хотели узнать?", bg.GetButtons());
            }
        }
    }
}
