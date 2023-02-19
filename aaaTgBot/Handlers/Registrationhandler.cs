using aaaSystemsCommon.Models.Difinitions;
using aaaTgBot.Data.Models;
using aaaTgBot.Messages;
using aaaTgBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TgBotLibrary;

namespace aaaTgBot.Handlers
{
    public class RegistrationHandler : BaseSepcialHandlerNew
    {
        private readonly long chatId;
        private BotService bot;
        private RegistrationModel model;

        public RegistrationHandler(long chatId)
        {
            bot = new BotService(chatId);
            this.chatId = chatId;
            model = new();
        }

        public override async Task ProcessMessage(Message message)
        {
            if (message.From.IsBot) { await StartProcessing(); }
            await base.ProcessMessage(message);
        }

        protected override async Task RegistrateProcessing()
        {
            await SendRequestName();
            await SendRequestPhone();
            await CompleteRegistration();
        }

        private async Task SendRequestName()
        {
            await bot.SendMessage("Имя");
            var name = await OnMessage();

            while (name.Text.Length < 2)//TODO: валидация
            {
                await bot.SendMessage("Имя");
                name = await OnMessage();
            }

            model.Name = name.Text;
        }

        private async Task SendRequestPhone()
        {
            await bot.SendMessage("Номер");
            var phone = await OnMessage();

            while (phone.Text.Length < 10) //TODO: валидация
            {
                await bot.SendMessage("Номер");
                phone = await OnMessage();
            }

            model.Phone = phone.Text;
            await bot.SendMessage("Ok");
        }

        private async Task CompleteRegistration()
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
