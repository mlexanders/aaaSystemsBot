using aaaTgBot;
using aaaTgBot.Handlers;
using aaaTgBot.Services;
using TgBotLibrary;

BaseBotSettings.SetSettings(AppSettings.BotToken, AppSettings.BackRoot);
await TgBotClient.StartBot(UpdateHandler.HandleUpdateAsync, null!);
