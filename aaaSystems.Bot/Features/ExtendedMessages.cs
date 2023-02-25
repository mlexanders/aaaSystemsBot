using Telegram.Bot.Types.ReplyMarkups;

namespace aaaSystems.Bot.Features
{
    internal abstract class ExtendedMessages : CommonMessages
    {
        private int? callbackMessageId;

        internal ExtendedMessages(long chatId, int? callbackMessageId = null) : base(chatId)
        {
            this.callbackMessageId = callbackMessageId;
        }

        internal Task TryEdit(string text, IReplyMarkup markup = null!)
        {
            try
            {
                return bot.EditMessage(callbackMessageId!.Value, text, markup);
            }
            catch
            {
                return bot.SendMessage(text, markup);
            }
            finally
            {
                callbackMessageId = null;
            }
        }
    }
}
