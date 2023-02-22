using aaaSystems.Bot.Data.Entity;
using Telegram.Bot.Types;

namespace aaaSystems.Bot.Features.Other
{
    internal class Administrator : BaseSender
    {
        private readonly SenderEntity sender;

        public Administrator(SenderEntity sender)
        {
            this.sender = sender;
        }

        public override Task Processing(Update update)
        {
            return base.Processing(update);
        }
    }
}
