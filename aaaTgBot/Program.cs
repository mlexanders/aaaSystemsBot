using aaaTgBot;
using aaaTgBot.Handlers;
using TgBotLibrary;


await TgBotClient.StartBot(AppSettings.BotToken, UpdateHandler.HandleUpdateAsync, null!);
