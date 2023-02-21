using aaaSystems.Bot.Data.Interfaces;
using Telegram.Bot.Types;

namespace aaaSystems.Bot.Features
{
    internal abstract class BaseSender : ISender
    {
        public virtual Task Processing(Update update)
        {
            throw new NotImplementedException();
        }
    }
}
