using aaaSystems.Bot.Data.Interfaces;
using Telegram.Bot.Types;

namespace aaaSystems.Bot.Features.Other
{
    internal abstract class BaseSender : ISender
    {
        public virtual Task Processing(Update update)
        {
            throw new NotImplementedException();
        }
    }
}
