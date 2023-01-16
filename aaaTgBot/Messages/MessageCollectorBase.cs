using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Utils;
using aaaTgBot.Data;
using aaaTgBot.Data.Exceptions;
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
       
        public async Task SendUnknownUserMessage()
        {
            var button = new ButtonsGenerator();
            button.SetInlineButtons((InlineButtonsTexts.Forward));
            await botService.SendMessage(Texts.UnknownUser, button.GetButtons());
        }

        public async Task JoinToRoom(Message message, long clientChatId)
        {
            try
            {
                var usersService = TransientService.GetUsersService();
                var user = await usersService.GetByChatId(chatId) ?? throw new UserNotFound(chatId);
                
                if (user.Role is Role.Admin)
                {
                    if (UpdateHandler.BusyUsersIdAndService.TryGetValue(clientChatId, out var handler))
                    {
                        await botService.SendMessage(Texts.InfoMessageForAdmin("name"));
                        await handler.ProcessMessage(message); // возможно это не нужно, тк в основном обработчике дальше вызов ProcessMessage
                    }
                    else
                    {
                        await botService.SendMessage("Собеседник завершил диалог");
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
            catch (UserNotFound e)
            {
             //   await TryToStartRegistration(); TODO : ref to registration
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
