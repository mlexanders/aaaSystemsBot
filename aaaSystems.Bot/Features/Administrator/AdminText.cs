using aaaSystemsCommon.Entity;
using Telegram.Bot.Types;

namespace aaaSystems.Bot.Features.Administrator
{
    internal static class AdminText
    {
        internal const string Start = "Стартовое сообщение";
        internal const string Menu = "Меню";
        internal const string InfoMessage = "Все сообщения будут пересылаться собеседнику, чтобы прекратить общение отправьте команду /leave";
        internal const string AllUsers = "Список всех пользователей";
    }

    internal static class AdminCallback
    {
        #region Menu

        internal const string Requests = "requests";
        internal const string Users = "Users";
        internal const string Settings = "Settings";

        internal readonly static (string, string)[] Menu = { ("Обращения", Requests), ("Пользователи", Users), ("Настройки", Settings) };
        internal const string Good = "Хорошо";

        #endregion

        #region NotificationMenu

        internal const string Write = "Написать";
        internal const string LoadDialog = "Показать сообщения";
        internal const string MoreDetails = "Подробнее";

        internal static (string, string) GetWriteButton(long chatId) => (Write, $"{Write}:{chatId}");
        internal static (string, string) GetLoadDialogButton(long chatId) => (LoadDialog, $"{LoadDialog}:{chatId}");
        internal static (string, string) GeMoreDetailsButton(long chatId) => (MoreDetails, $"{MoreDetails}:{chatId}");
        internal static (string, string) GetDialogCallback(Dialog dialog) => (dialog.Sender.Name, $"Dialog:{dialog.ChatId}");

        internal static (string, string)[] GetNotificationMenu(long chatId)
        {
            (string, string)[] buttons =
            {
                GetWriteButton(chatId),
                GetLoadDialogButton(chatId),
                GeMoreDetailsButton(chatId)
            };
            return buttons;
        } 

        #endregion
    }
}
