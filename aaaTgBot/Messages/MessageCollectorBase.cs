using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Utils;
using aaaTgBot.Data;
using aaaTgBot.Handlers;
using aaaTgBot.Services;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = aaaSystemsCommon.Models.User;

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
            else if (user.Role is Role.User)
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

        public async Task SendRoomList()
        {
            var roomService = TransientService.GetRoomsService();
            var userService = TransientService.GetUsersService();
            var bg = new ButtonsGenerator();

            var rooms = await roomService.Get();

            if (rooms.Any())
            {
                string msg = null!;
                User user = null!;

                foreach (var room in rooms)
                {
                    user = await userService.Get(room.ClientId);

                    bg.SetInlineButtons(($"↪ {user.Name} - {user.Phone}", $"SendMessagesRoom:{user.ChatId}"));
                    msg += $"- {user.Name} \n";
                }
                await botService.SendMessage(msg, bg.GetButtons());
            }
            else
            {
                await botService.SendMessage(Texts.NoApplications);
            }
        }

        public async Task SendMessagesRoom(long chatId, long clientChatId)
        {
            var roomService = TransientService.GetRoomsService();
            var userservice = TransientService.GetUsersService();
            var room = await roomService.GetByChatId(clientChatId);

            if (room != null)
            {
                if (room.RoomMessages != null)
                {
                    foreach (var msg in room.RoomMessages)
                    {
                         botService.Forward(chatId, clientChatId, msg.MessageId, true); // без await работает, иначе код 400
                    }
                    //await botService.Forward(chatId, clientChatId, 3057, true);
                }
                else
                {
                    await botService.SendMessage("Сообщений пока нет");
                }
                var bg = new ButtonsGenerator();
                bg.SetInlineButtons((InlineButtonsTexts.Write, $"JoinToRoom:{clientChatId}"));

                var user = await userservice.Get(room.ClientId);
                await botService.FromBotMessage(Texts.InfoMessageForAdmin(user.Name ?? " Без имени 🙅‍"), bg.GetButtons());
            }
            else
            {
                await botService.SendMessage("Комната не найдена");
            }
        }

        public async Task JoinToRoom(Message message, long clientChatId)
        {
            var usersService = TransientService.GetUsersService();
            var user = await usersService.GetByChatId(chatId);

            try
            {
                if (user.Role is Role.Admin)
                {
                    await botService.SendMessage(Texts.InfoMessageForAdmin("name"));

                    if (UpdateHandler.BusyUsersIdAndService.TryGetValue(clientChatId, out var handler))
                    {
                        await handler.ProcessMessage(message);
                    }
                }
                else if (user.Role is Role.User)
                {
                    await botService.SendMessage(Texts.InfoMessageForAdmin("name"));
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
            catch (ArgumentException e) //TODO : exceptions
            {
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion
    }
}
