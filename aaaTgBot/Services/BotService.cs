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
            await SaveExecute(bot.SendTextMessageAsync(chatId, text));
        }

        public async Task SendMessage(string text, IReplyMarkup markup)
        {
            await SaveExecute(bot.SendTextMessageAsync(chatId, text, replyMarkup: markup));
        }

        public async Task SendMessage(string text, ParseMode? parseMode = null, bool? disableWebPagePreview = null, bool? disableNotification = null, ReplyKeyboardMarkup markup = null)
        {
            await SaveExecute(bot.SendTextMessageAsync(
                chatId,
                text,
                parseMode: parseMode,
                disableWebPagePreview: disableWebPagePreview,
                disableNotification: disableNotification,
                replyMarkup: markup));
        }
        #endregion

        public async Task DeleteMessage(int messageId)
        {
            await SaveExecute(bot.DeleteMessageAsync(chatId, messageId));
        }
        public async Task EditMessage(int messageId, string text, IReplyMarkup? markup = null)
        {
            await SaveExecute(bot.EditMessageTextAsync(chatId, messageId, text, replyMarkup: (InlineKeyboardMarkup)markup));
        }

        public async Task Forward(long chatId, long fromChatId, int messageId, bool? disableNotification = null)
        {
            await SaveExecute(bot.ForwardMessageAsync(chatId, fromChatId, messageId, disableNotification: disableNotification));
        }

        public async Task FromBotMessage(string text, IReplyMarkup markup)
        {
            await SaveExecute(bot.SendTextMessageAsync(chatId, $"<b>От : {await bot.GetMeAsync()}</b> \n {text}", ParseMode.Html, replyMarkup: markup));
        }

        private static async Task SaveExecute(Task task)
        {
            try
            {
                await task;
            }
            catch (Exception e)
            {
                Console.WriteLine(e); //TODO:  logger
            }
        }
    }
}