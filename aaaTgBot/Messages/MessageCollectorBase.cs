using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Utils;
using aaaTgBot.Data;
using aaaTgBot.Handlers;
using User = aaaSystemsCommon.Models.User;
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
            buttonsGenerator.SetInlineButtons(InlineButtonsTexts.Forward);
            await botService.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public async Task SendInfoMessageAndGoToRoom(Message message)
        {
            await botService.SendMessage(Texts.InfoMessage);

            UpdateHandler.BusyUsersIdAndService.Add(message.Chat.Id, new RoomHandler(message.Chat.Id));
        }

        public async Task SendUserInfo(long? otherChatId = null, IReplyMarkup markup = null!)
        {
            var user = await TransientService.GetUsersService().GetByChatId(otherChatId ?? chatId);
            var msg = user.GetInfo();
            await botService.SendMessage(msg, markup);
        }


        public async Task SendMessage(string text)
        {
            await botService.SendMessage(text);
        }

        public async Task SendSubmitAnApplication()
        {
            var bg = new ButtonsGenerator();
            bg.SetInlineButtons(InlineButtonsTexts.Write);

            await botService.SendMessage(Texts.SubmitAnApplication, bg.GetButtons());
        }
        #region PleaseFix

        public async Task TryToStartRegistration()
        {
            var user = await TransientService.GetUsersService().GetByChatId(chatId);

            if (user is null)
            {
                UpdateHandler.BusyUsersIdAndService.Add(chatId, new RegistrationHandler(chatId));
            }
            else if (user.Role is Role.Admin)
            {
                await SendMenu();
            }
            else
            {
                await SendSubmitAnApplication();
            }
        }

        public async Task SendMenu()
        {
            var bg = new ButtonsGenerator();
            bg.SetInlineButtons(InlineButtonsTexts.Rooms);

            await botService.SendMessage("Что бы вы хотели узнать?", bg.GetButtons());
        }


        public async Task SendListRoom()
        {
            var roomService = TransientService.GetRoomsService();
            var userService = TransientService.GetUsersService();
            var bg = new ButtonsGenerator();

            var rooms = await roomService.Get();

            if (rooms != null)
            {
                string msg = null!;
                User user = null!;

                foreach (var room in rooms)
                {
                    user = await userService.Get(room.ClientId);

                    bg.SetInlineButtons(($"↪ {user.Name} - {user.Phone}", $"GetRoom:{user.ChatId}"));
                    msg += $"{user.GetInfo()} \n";
                }
                await botService.SendMessage(msg, bg.GetButtons());
            }
        } 
        #endregion
    }
}
