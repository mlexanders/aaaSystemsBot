namespace aaaSystems.Bot.Features.Administrator
{
    internal static class AdminText
    {
        internal const string Start = "Стартовое сообщение";
        internal const string Menu = "Меню";
        internal const string AllRequestsMessage = "Тут все обращения клиентов";
        internal const string AllUsers = "Список всех пользователей";
    }

    internal static class AdminCallback
    {
        #region Menu
        internal const string requests = "requests";
        internal const string users = "users";
        internal const string settings = "settings";
        #endregion

        internal readonly static (string, string)[] Menu = { ("Обращения", requests), ("Пользователи", users), ("Настройки", settings) };
        internal const string Good = "Хорошо";
    }
}
