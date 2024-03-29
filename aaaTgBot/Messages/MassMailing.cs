﻿using aaaSystemsCommon.Utils;
using aaaTgBot.Data;
using aaaTgBot.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotLibrary;
using User = aaaSystemsCommon.Models.User;

namespace aaaTgBot.Messages
{
    public static class MassMailing
    {
        private static readonly TelegramBotClient bot = TgBotClient.BotClient;

        public static async Task SendNotificateMessage(List<long> chatIds, User client, Message message)
        {
            var msg = $" Обращение \n" +
                      $"{client.GetInfo()} \n" +
                      $"Сообщает: \n";

            LogService.LogInfo(msg);

            var bg = new ButtonsGenerator();
            bg.SetInlineButtons(($"↪ Загрузить диалог", CallbackData.GetSendMessagesRoom(client.Id)));
            bg.SetInlineButtons(($"↪ Написать", CallbackData.GetJoinToRoom(client.Id)));

            foreach (var chatId in chatIds)
            {
                await bot.SendTextMessageAsync(chatId, msg, replyMarkup: bg.GetButtons());
                await bot.ForwardMessageAsync(chatId, message.Chat.Id, message.MessageId);
            }
        }

        public static async Task ForwardMessageToUsers(List<long> chatIds, Message message)
        {
            foreach (var chatId in chatIds)
            {
                await bot.ForwardMessageAsync(chatId, message.Chat.Id, message.MessageId, true);
            }
        }
    }
}