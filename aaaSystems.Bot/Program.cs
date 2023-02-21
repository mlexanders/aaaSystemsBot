using aaaSystems.Bot;
using aaaSystems.Bot.Handlers;
using TelegramBotLib;


var bot = new BotClient(new UpdateHandler(), AppSettings.BotToken, AppSettings.BackRoot);
bot.Start();