using aaaTgBot.Handlers;
using TgBotLibrary;


await TgBotClient.StartBot(AppSettings.BotToken, Distributor.Distribute, null!);
