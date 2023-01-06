using aaaTgBot;
using aaaTgBot.Services;
using TgBotLibrary;


await TgBotClient.StartBot(AppSettings.BotToken, UpdateHandler.HandleUpdateAsync, null!);
