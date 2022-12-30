using aaaTgBot.Handlers;
using TgBotLibrary;


await Bot.StartBot(AppSettings.BotToken, Distributor.Distribute, null!);
