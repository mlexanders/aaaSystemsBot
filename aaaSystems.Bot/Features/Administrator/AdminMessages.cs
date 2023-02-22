using aaaSystems.Bot.Data.Texts;

namespace aaaSystems.Bot.Features.Administrator
{
    internal class AdminMessages : ExtendedMessages
    {
        public AdminMessages(long chatId, int? callbackMessageId = null) : base(chatId, callbackMessageId) { }

        public override async Task SendStartMessage()
        {
            buttonsGenerator.SetInlineButtons(AdminCallback.Good);
            await bot.SendMessage(Texts.StartMessage, buttonsGenerator.GetButtons());
        }

        public override async Task SendMenu()
        {
            buttonsGenerator.SetInlineButtons(AdminCallback.Menu);
            await TryEdit(AdminText.Menu);
        }

        public async Task ShowRequests()
        {
            buttonsGenerator.SetInlineButtons("user1", "user2", "ratatyi"); // TODO : инфа об обращениях, в кнопках калбеки на диалог с клиентом.
            await TryEdit(AdminText.AllRequestsMessage);
        }

        public async Task ShowAllUsers()
        {
            buttonsGenerator.SetInlineButtons(AdminText.AllUsers); // TODO : вывести список пользователей. => где после выбора можно будет посмотреть более детальную инфу
            await TryEdit(AdminText.AllUsers);
        }

        // TODO : вывод детальной информации, изменение роли.
        public async Task ShowUserInfo()
        {
            buttonsGenerator.SetInlineButtons(AdminText.AllUsers);
            await TryEdit(AdminText.AllUsers);
        }
    }
}
