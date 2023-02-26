using aaaSystems.Bot.Data.Texts;
using aaaSystems.Bot.Handlers;
using aaaSystems.Bot.Services;
using aaaSystemsCommon.Entity;

namespace aaaSystems.Bot.Features.Administrator
{
    internal class AdminMessages : ExtendedMessages
    {
        public AdminMessages(long chatId, int? callbackMessageId = null) : base(chatId, callbackMessageId) { }

        internal override async Task SendStartMessage()
        {
            buttonsGenerator.SetInlineButtons(AdminCallback.Good);
            await bot.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        internal override async Task SendMenu()
        {
            buttonsGenerator.SetInlineButtons(AdminCallback.Menu);
            await TryEdit(AdminText.Menu);
        }

        internal async Task ShowDialogs()
        {
            var dialogsService = TransientService.GetDialogsService();
            var dialogs = await dialogsService.Get();

            foreach (var dialog in dialogs)
            {
                buttonsGenerator.SetInlineButtons(AdminCallback.GetDialogCallback(dialog));
            }
            buttonsGenerator.SetGoBackButton();

            await TryEdit(AdminText.InfoMessage);
        }

        internal async Task StartDialog(long chatId)
        {
            if (UpdateHandler.HandlingSenders.TryGetValue(chatId, out var handler))
            {
                UpdateHandler.HandlingSenders.Add(this.chatId, handler);
            }
            await TryEdit(AdminText.InfoMessage);
        }

        internal async Task LoadDialog(long dialogId)
        {
            var dialogMessagesService = TransientService.GetDialogMessagesService();

            var messages = await dialogMessagesService.GetByDialogId(dialogId);

            new Thread(() =>
            {
                foreach (var message in messages)
                {
                    bot.Forward
                }
            });
        }

        #region Senders

        public async Task ShowAllSenders()
        {
            buttonsGenerator.SetInlineButtons(AdminText.AllUsers); // TODO : вывести список пользователей. => где после выбора можно будет посмотреть более детальную инфу
            await TryEdit(AdminText.AllUsers);
        }

        // TODO : вывод детальной информации, изменение роли.
        public async Task ShowSenderInfo()
        {
            buttonsGenerator.SetInlineButtons(AdminText.AllUsers);
            await TryEdit(AdminText.AllUsers);
        }

        #endregion
    }
}
