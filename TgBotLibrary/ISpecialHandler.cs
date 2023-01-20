using Telegram.Bot.Types;

namespace TgBotLibrary
{
    public interface ISpecialHandler
    {
        Task ProcessMessage(Message message);
    }
}