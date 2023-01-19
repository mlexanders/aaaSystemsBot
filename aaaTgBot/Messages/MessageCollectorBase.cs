using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Utils;
using aaaTgBot.Data;
using aaaTgBot.Data.Exceptions;
using aaaTgBot.Handlers;
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
            buttonsGenerator.SetInlineButtons(InlineButtonsTexts.Registration);
            await botService.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public async Task SendUserInfo(long? otherChatId = null, IReplyMarkup markup = null!)
        {
            var user = await TransientService.GetUsersService().Get(otherChatId ?? chatId);
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
            button.SetInlineButtons((InlineButtonsTexts.Registration));
            await botService.SendMessage(Texts.UnknownUser, button.GetButtons());
        }

        public async Task SendUnknownMessage()
        {
            var button = new ButtonsGenerator();
            button.SetInlineButtons((InlineButtonsTexts.Menu));
            await botService.SendMessage(Texts.UnknownMessage, button.GetButtons());
        }

        public async Task SendMenu()
        {
            await SendStartMessage();
        }

        public async Task JoinToRoom(Message message, long clientChatId)
        {
            try
            {
                var usersService = TransientService.GetUsersService();
                var user = await usersService.Get(chatId) ?? throw new UserNotFound(chatId);

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
            catch (ArgumentException e) //TODO : exceptions
            {
                LogService.LogError(e.Message);
            }
            catch (UserNotFound e)
            {
                LogService.LogWarn("UserNotFound" + e.Message);
            }
            catch (Exception e)
            {
                LogService.LogError(e.Message);
            }
        }
    }
}
