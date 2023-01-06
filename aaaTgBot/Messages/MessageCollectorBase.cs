using aaaTgBot.Data;
using aaaTgBot.Services;
using Telegram.Bot.Types.ReplyMarkups;

namespace aaaTgBot.Handlers
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
            buttonsGenerator.SetInlineButtons(InlineButtonsTexts.Forward);
            await botService.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public async Task SendInfoMessage()
        {
            var response = TransientService.GetUsersService().GetByChatId(chatId) != null ? botService.SendMessage(Texts.InfoMessage) : TryToStartRegistration();
            await response;
        }

        public async Task SendUserInfo(long? otherChatId = null, IReplyMarkup markup = null!)
        {
            var user = await TransientService.GetUsersService().GetByChatId(otherChatId ?? chatId);
            var msg = $"{user.Name} \n" +
                      $"{user.Phone} \n" +
                      $"{user.Role}";
            await botService.SendMessage(msg, markup);
        }

        public async Task SendMessage(string text)
        {
            await botService.SendMessage(text);
        }

        public async Task TryToStartRegistration()
        {
            if (await TransientService.GetUsersService().GetByChatId(chatId) is null)
            {
                UpdateHandler.BusyUsersIdAndService.Add(chatId, new RegistrationHandler(chatId));
            }
            else
            {
                await SendInfoMessage();
            }
        }
    }
}
