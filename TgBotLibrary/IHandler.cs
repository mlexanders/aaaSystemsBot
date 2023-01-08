using Telegram.Bot.Types;

namespace TgBotLibrary
{
    public interface IHandler
    {
        Task ProcessMessage(Message message);
    }
}