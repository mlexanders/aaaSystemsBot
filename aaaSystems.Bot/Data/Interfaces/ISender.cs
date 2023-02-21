using Telegram.Bot.Types;

namespace aaaSystems.Bot.Data.Interfaces
{
    internal interface ISender
    {
        Task Processing(Update update);
    }
}
