using aaaSystems.Bot.Data;
using aaaSystems.Bot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBotLib.Handlers;

namespace aaaSystems.Bot.Handlers
{
    public class RegistrationHandler : BaseSepcialHandler
    {
        private readonly long chatId;
        private readonly BotService bot;
        private RegistrationModel model;

        public RegistrationHandler(long chatId)
        {
            bot = new BotService(chatId);
            this.chatId = chatId;
            model = new();
        }

        public override Task ProcessUpdate(Update update)
        {
            return base.ProcessUpdate(update);
        }

        public override Task ProcessCallbackQuery(CallbackQuery callbackQuery) => Task.CompletedTask; // ignore callBack

        protected override async Task RegistrateProcessing()
        {
            await SendRequestName();
            await SendRequestPhone();
            //await CompleteRegistration();
        }

        private async Task SendRequestName()
        {
            await bot.SendMessage("Имя");
            model.Name = (await GetMessage()).Text;

            while (!model.NameIsValid())
            {
                await bot.SendMessage("Введите имя еще раз");
                model.Name = (await GetMessage()).Text;
            }
        }

        private async Task SendRequestPhone()
        {
            await bot.SendMessage("Введите контактный телефон", ButtonsGenerator.GetKeyboardButtonWithPhoneRequest("Отправить телефон"));
            SetPhone(await GetMessage());

            while (!model.PhoneIsValid())
            {
                await bot.SendMessage("Введите номер телефона еще раз");
                SetPhone(await GetMessage());
            }

            void SetPhone(Message message)
            {
                model.Phone = message.Type switch
                {
                    MessageType.Text => message.Text,
                    MessageType.Contact => message.Contact?.PhoneNumber,
                    _ => null!
                };
            }
        }

        //private async Task CompleteRegistration()
        //{
        //    try
        //    {
        //        var usersService = TransientService.GetUsersService();
        //        await usersService.Post(new User()
        //        {
        //            Id = chatId,
        //            Name = model.Name,
        //            Phone = model.Phone,
        //            Role = Role.User
        //        });
        //        await new MessageCollectorBase(chatId).SendUserInfo(markup: new ReplyKeyboardRemove());
        //    }
        //    catch (HttpRequestException e)
        //    {
        //        LogService.LogServerNotFound(e.Message);
        //        await bot.SendMessage(e.Message);
        //    }
        //    finally
        //    {
        //        UpdateHandler.BusyUsersIdAndService.Remove(chatId);

        //        var messageCollector = new MessageCollectorBase(chatId);
        //        await messageCollector.SendMenu();
        //    }
        //}
    }
}
