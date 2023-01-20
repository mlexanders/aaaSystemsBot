using aaaSystemsCommon.Models.Difinitions;
using aaaTgBot.Data.Models;
using aaaTgBot.Messages;
using aaaTgBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBotLibrary;

namespace aaaTgBot.Handlers
{
    public class RegistrationHandler : BaseSpecialHandler
    {
        private RegistrationModel model;
        private string registrationMessage;
        private readonly long chatId;

        public RegistrationHandler(long chatId) : base(new BotService(chatId))
        {
            model = new();
            this.chatId = chatId;
        }

        public override async Task ProcessMessage(Message registrationMessage)
        {
            this.registrationMessage = registrationMessage.Type switch
            {
                MessageType.Contact => registrationMessage.Contact!.PhoneNumber!,
                MessageType.Text => registrationMessage.Text!,
                _ => null!
            };

            if (registrationMessage is null) currentTask.Start();
            else await base.ProcessMessage(registrationMessage);
        }

        protected override void RegistrateProcessing()
        {
            AddProcessing("Как к вам обращаться?", () => model.Name = registrationMessage);
            AddProcessing("Контактный телефон", () => model.Phone = registrationMessage, CompleteRegistration, button: ButtonsGenerator.GetKeyboardButtonWithPhoneRequest("Отправить телефон"));
        }

        private async void CompleteRegistration()
        {
            try
            {
                var usersService = TransientService.GetUsersService();
                await usersService.Post(new aaaSystemsCommon.Models.User()
                {
                    Id = chatId,
                    Name = model.Name,
                    Phone = model.Phone,
                    Role = Role.User
                });
                await new MessageCollectorBase(chatId).SendUserInfo(markup: new ReplyKeyboardRemove());
            }
            catch (HttpRequestException e)
            {
                LogService.LogServerNotFound(e.Message);
                await bot.SendMessage("Ошибка");
            }
            finally
            {
                UpdateHandler.BusyUsersIdAndService.Remove(chatId);

                var messageCollector = new MessageCollectorBase(chatId);
                await messageCollector.SendMenu();
            }
        }
    }
}
