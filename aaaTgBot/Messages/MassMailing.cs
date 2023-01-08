using aaaSystemsCommon.Utils;
using aaaTgBot.Services;
using Telegram.Bot;
using TgBotLibrary;
using User = aaaSystemsCommon.Models.User;

namespace aaaTgBot.Messages
{
    public static class MassMailing
    {
        private static readonly TelegramBotClient bot = TgBotClient.BotClient;

        public static async Task SendNotificateMessage(List<long> chatIds, User client, string text)
        {
            var msg = $" Обращение \n" +
                      $"{client.GetInfo()} \n" +
                      $"Сообщает: \n" +
                      $"{text}";

            var bg = new ButtonsGenerator();
            bg.SetInlineButtons(($"↪ Загрузить диалог", $"GetRoom:{client.ChatId}"));


            foreach (var chatId in chatIds)
            {
                await bot.SendTextMessageAsync(chatId, msg, replyMarkup: bg.GetButtons()); ;
            }
        }
    }
}