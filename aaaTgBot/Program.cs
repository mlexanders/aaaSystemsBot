using Telegram.Bot;
using Telegram.Bot.Types;
using TgBotLibrary;

var botToken = "5728212844:AAHpn4rwbTuJYe7OnjkoT1YXa4E3SwWBUr4";

await Bot.StartBot(botToken, HandleUpdateAsync, null!);

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if (update.Message is not { } message)
        return;

    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
}
