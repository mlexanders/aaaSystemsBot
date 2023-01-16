using aaaSystemsCommon.Models;
using aaaSystemsCommon.Models.Difinitions;
using aaaTgBot.Data;
using aaaTgBot.Data.Exceptions;
using aaaTgBot.Handlers;
using aaaTgBot.Services;

namespace aaaTgBot.Messages
{
    public class MessageCollector : MessageCollectorBase
    {
        private readonly int messageId;

        public MessageCollector(long chatId, int messageId) : base(chatId)
        {
            this.messageId = messageId;
        }

        public async Task DeleteMessage()
        {
            await botService.DeleteMessage(messageId);
        }

        public async Task TryToStartRegistration()
        {
            var user = await TransientService.GetUsersService().GetByChatId(chatId);

            if (user == null)
            {
                UpdateHandler.BusyUsersIdAndService.Add(chatId, new RegistrationHandler(chatId));
            }
            else
            {
                await EditToMenu(user);
            }
        }

        public async Task EditToMenu()
        {
            var user = await TransientService.GetUsersService().GetByChatId(chatId);
            await EditToMenu(user);
        }

        private async Task EditToMenu(User user)
        {
            var bg = new ButtonsGenerator();

            if (user.Role is Role.User)
            {
                bg.SetInlineButtons(InlineButtonsTexts.Write);
                await botService.EditMessage(messageId, Texts.SubmitAnApplication, bg.GetButtons());
            }
            else if (user.Role is Role.Admin)
            {
                bg.SetInlineButtons(InlineButtonsTexts.Rooms);
                await botService.EditMessage(messageId, "Что бы вы хотели узнать?", bg.GetButtons());
            }
        }

        #region ForAdmins

        public async Task EditToRoomList()
        {
            var roomService = TransientService.GetRoomsService();
            var userService = TransientService.GetUsersService();
            var buttonGenerator = new ButtonsGenerator();

            var rooms = await roomService.Get();

            if (rooms.Any())
            {
                var msg = string.Empty;
                User user;

                foreach (var room in rooms)
                {
                    user = await userService.Get(room.ClientId);

                    buttonGenerator.SetInlineButtons(($"↪ {user.Name} - {user.Phone}", $"SendMessagesRoom:{user.ChatId}"));
                    msg += $"- {user.Name} \n";
                }

                buttonGenerator.SetGoBackButton(InlineButtonsTexts.Menu);
                await botService.EditMessage(messageId, msg, buttonGenerator.GetButtons());
            }
            else
            {
                await botService.EditMessage(messageId, Texts.NoApplications);
            }
        }

        public async Task SendMessagesRoom(long chatId, long clientChatId)
        {
            try
            {
                var bg = new ButtonsGenerator();
                var roomService = TransientService.GetRoomsService();
                var userservice = TransientService.GetUsersService();
                var room = await roomService.GetByChatId(clientChatId) ?? throw new RoomNotFound("Комната не найдена");

                if (!(room.RoomMessages != null && room.RoomMessages.Any())) throw new MessageNotFound("Сообщений пока нет");

                await DeleteMessage();

                foreach (var msg in room.RoomMessages)
                {
                    await botService.Forward(chatId, msg.ChatId, msg.MessageId, true);
                }

                bg.SetInlineButtons((InlineButtonsTexts.Write, $"JoinToRoom:{clientChatId}")); // TODO: callback data
                bg.SetGoBackButton(InlineButtonsTexts.Rooms);

                var user = await userservice.Get(room.ClientId);
                await botService.FromBotMessage(Texts.InfoMessageForAdmin(user.Name ?? " Без имени 🙅‍"), bg.GetButtons());
            }
            catch (RoomNotFound e)
            {
                await botService.SendMessage(e.Message);
            }
            catch (MessageNotFound e)
            {
                await botService.SendMessage(e.Message);
            }
        }


        #endregion ForAdmins
    }
}