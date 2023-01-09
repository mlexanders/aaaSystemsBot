using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBotLibrary;

namespace aaaTgBot.Services
{
    public class BotService : IBotService
    {
        private readonly long chatId;
        private readonly TelegramBotClient bot;

        public BotService(long chatId)
        {
            bot = TgBotClient.BotClient;
            this.chatId = chatId;
        }

        #region SendMessage

        public async Task SendMessage(string text)
        {
            await bot.SendTextMessageAsync(chatId, text);
        }

        public async Task SendMessage(string text, IReplyMarkup markup)
        {
            await bot.SendTextMessageAsync(chatId, text, replyMarkup: markup);
        }

        public async Task SendMessage(string text, ParseMode? parseMode = null, bool? disableWebPagePreview = null, bool? disableNotification = null, ReplyKeyboardMarkup markup = null)
        {
            await bot.SendTextMessageAsync(chatId, text, parseMode: parseMode, disableWebPagePreview: disableWebPagePreview, disableNotification: disableNotification, replyMarkup: markup);
        }
        #endregion

        public async Task DeleteMessage(int messageId)
        {
            await bot.DeleteMessageAsync(chatId, messageId);
        }

        public Task Forward(long chatId, long fromChatId, int messageId, bool? disableNotification = null)
        {
            return bot.ForwardMessageAsync(chatId, fromChatId, messageId, disableNotification: disableNotification);
        }

        public async Task FromBotMessage(string text, IReplyMarkup markup)
        {
            await bot.SendTextMessageAsync(chatId, $"<b>От : {await bot.GetMeAsync()}</b> \n {text}", ParseMode.Html, replyMarkup: markup);
        }
    }
}