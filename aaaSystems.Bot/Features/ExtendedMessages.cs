﻿using Telegram.Bot.Types.ReplyMarkups;

namespace aaaSystems.Bot.Features
{
    internal abstract class ExtendedMessages : CommonMessages
    {
        private int? callbackMessageId;

        protected ExtendedMessages(long chatId, int? callbackMessageId = null, string callback = null!) : base(chatId)
        {
            this.callbackMessageId = callbackMessageId;
        }

        protected Task TryEdit(string text, IReplyMarkup markup = null!)
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
