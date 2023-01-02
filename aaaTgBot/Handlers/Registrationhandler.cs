using aaaTgBot.Data;
using aaaTgBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TgBotLibrary;

namespace aaaTgBot.Handlers
{
    public class RegistrationHandler : BaseSpecialHandler
    {
        public RegistrationModel model = new();
        private string registrationMessage;
        private long chatId;
        private bool isSkipCurrentAction;

        public RegistrationHandler(long chatId) : base(new BotService(chatId))
        {
            this.chatId = chatId;
        }

        public override async Task ProcessMessage(Message registrationMessage)
        {
            isSkipCurrentAction = registrationMessage.ReplyMarkup?.InlineKeyboard.First().First().CallbackData == InlineButtonsTexts.SkipInput.Item2;
            
            this.registrationMessage = registrationMessage.Type == MessageType.Contact ?
                registrationMessage.Contact.PhoneNumber! : registrationMessage.Text;
            await base.ProcessMessage(registrationMessage);
        }

        protected override void RegistrateProcessing()
        {
            AddProcessing("Как к вам обращаться? ", () => model.Name = registrationMessage);
            AddProcessing("Контактный телефон ", () => model.Phone = registrationMessage, button: ButtonsGenerator.GetKeyboardButtonWithPhoneRequest("Отправить телефон"));
            var bg = new ButtonsGenerator();
            bg.SetInlineButtons(InlineButtonsTexts.SkipInput);
            AddProcessing("Задайте вопрос или опишите вашу проблему", () => model.Additional = registrationMessage, button: bg.GetButtons());
            if (isSkipCurrentAction) AddProcessing("", () => model.Additional = model.Additional + " / " + registrationMessage, button: bg.GetButtons());

            AddProcessing("", () => model.Phone = registrationMessage, CompleteRegistration);
        }

        private async void CompleteRegistration()
        {
            try
            {
            }
            catch (HttpRequestException)
            {
            }
            finally
            {
                DistributionService.BusyUsersIdAndService.Remove(chatId);
            }
        }
    }
}
