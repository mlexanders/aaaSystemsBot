﻿using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TgBotLibrary;

namespace aaaTgBot.Services
{
    public class BotService
    {
        private readonly long chatId;
        private readonly TelegramBotClient bot;

        public BotService(long chatId)
        {
            bot = TgBotClient.BotClient;
            this.chatId = chatId;
        }

        #region SendMessage

        public async Task SendMessageAsync(string text)
        {
            await bot.SendTextMessageAsync(chatId, text);
        }

        public async Task SendMessageAsync(string text, IReplyMarkup markup = null)
        {
            await bot.SendTextMessageAsync(chatId, text, replyMarkup: markup);
        }

        public async Task SendMessageAsync(string text, ParseMode? parseMode = null, bool? disableWebPagePreview = null, bool? disableNotification = null, ReplyKeyboardMarkup markup = null)
        {
            await bot.SendTextMessageAsync(chatId, text, parseMode: parseMode, disableWebPagePreview: disableWebPagePreview, disableNotification: disableNotification, replyMarkup: markup);
        }
        #endregion

        public async Task DeleteMessageAsync(int messageId)
        {
            await bot.DeleteMessageAsync(chatId, messageId);
        }



    }
}