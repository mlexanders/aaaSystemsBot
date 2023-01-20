using aaaTgBot;
using aaaTgBot.Handlers;
using TgBotLibrary;

BaseBotSettings.SetSettings(AppSettings.BotToken, AppSettings.BackRoot);
await TgBotClient.StartBot(UpdateHandler.HandleUpdateAsync, null!);
