using aaaSystemsCommon.Models.Difinitions;
using aaaTgBot.Data;
using aaaTgBot.Data.Exceptions;
using aaaTgBot.Handlers;
using aaaTgBot.Services;
using Telegram.Bot.Types;
using TgBotLibrary;
using User = aaaSystemsCommon.Models.User;

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
            var user = await TransientService.GetUsersService().Get(chatId);

            if (user == null)
            {
                UpdateHandler.BusyUsersIdAndService.Add(chatId, new RegistrationHandler(chatId));
            }
            else
            {
                await EditToMenu(user);
            }
        }

        public Task EditToMenu()
        {
            return TryToStartRegistration();
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

        public async Task JoinToRoom(Message message, long clientChatId)
        {
            try
            {
                var usersService = TransientService.GetUsersService();
                var user = await usersService.Get(chatId) ?? throw new UserNotFound(chatId);

                await botService.DeleteMessage(messageId);
                if (user.Role is Role.Admin)
                {
                    if (UpdateHandler.BusyUsersIdAndService.TryGetValue(clientChatId, out var handler))
                    {
                        var client = await usersService.Get(clientChatId);
                        await botService.SendMessage(Texts.InfoMessageForAdmin(client.Name));
                        await handler.ProcessMessage(message);
                    }
                    else
                    {
                        await botService.SendMessage("Собеседник завершил диалог");
                    }
                }
                else if (user.Role is Role.User)
                {
                    await botService.SendMessage(Texts.InfoMessage);

                    if (UpdateHandler.BusyUsersIdAndService.TryGetValue(clientChatId, out var handler))
                    {
                        await handler.ProcessMessage(message);
                    }
                    else
                    {
                        UpdateHandler.BusyUsersIdAndService.Add(chatId, new RoomHandler(chatId));
                    }
                }
            }
            catch (ArgumentException e)
            {
                LogService.LogError(e.Message);
            }
            catch (UserNotFound e)
            {
                UpdateHandler.BusyUsersIdAndService.Add(message.Chat.Id, new RegistrationHandler(message.Chat.Id));
                LogService.LogWarn($"UserNotFound : {e.Message}");
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message);
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
                    user = await userService.Get(room.UserId);
                    buttonGenerator.SetInlineButtons(($"↪ {user.Name} - {user.Phone}", CallbackData.GetSendMessagesRoom(user.Id)));
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
                    await botService.Forward(chatId, msg.UserId, msg.Id, true);
                }

                bg.SetInlineButtons((InlineButtonsTexts.Write, CallbackData.GetJoinToRoom(clientChatId)));
                bg.SetGoBackButton(InlineButtonsTexts.Rooms);

                var user = await userservice.Get(room.UserId);
                await botService.FromBotMessage(Texts.InfoMessageForAdmin(user.Name), bg.GetButtons());
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