namespace TelegramBotLib.Interfaces
{
    public interface IBaseSpecialHandler : IBaseHandler
    {
        Task StartProcessing();
    }
}
