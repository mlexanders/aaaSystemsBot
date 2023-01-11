﻿using aaaTgBot.Data;
using aaaTgBot.Messages;
using Telegram.Bot.Types;

namespace aaaTgBot.Handlers
{
    public static class MainHandler
    {
        public static async Task MessageProcessing(long chatId, Message message)
        {
            var messageCollector = new MessageCollector(chatId, message.MessageId);

            Task response = message.Text switch
            {
                "/start" => messageCollector.SendStartMessage(),
                _ => messageCollector.TryToStartRegistration(),
            };

            await response;
        }

        public static async Task CallbackQueryProcessing(long chatId, CallbackQuery callbackQuery)
        {
            var messageCollector = new MessageCollector(chatId, callbackQuery.Message.MessageId);

            Task response = callbackQuery.Data switch
            {
                "@" + InlineButtonsTexts.Forward => messageCollector.TryToStartRegistration(),
                "@" + SpecialCallbacks.Menu => messageCollector.EditToMenu(),
                "@" + InlineButtonsTexts.Rooms => messageCollector.EditToRoomList(),
                "@" + InlineButtonsTexts.Write => messageCollector.JoinToRoom(callbackQuery.Message, callbackQuery.Message.Chat.Id),
                _ => SpecialProcessing(callbackQuery, messageCollector)
            };
            await response;
        }

        private static Task SpecialProcessing(CallbackQuery callbackQuery, MessageCollector mc)
        {
            var data = callbackQuery.Data;
            if (string.IsNullOrWhiteSpace(data)) return Task.CompletedTask;

            var clientChatId = Convert.ToInt64(string.Join("", data.Where(c => char.IsDigit(c))));
            
            if (data.Contains("SendMessagesRoom")) return mc.SendMessagesRoom(callbackQuery.Message.Chat.Id, clientChatId);
            else if (data.Contains("JoinToRoom")) return mc.JoinToRoom(callbackQuery.Message, clientChatId); 
            else return Task.CompletedTask;
        }
    }
}
