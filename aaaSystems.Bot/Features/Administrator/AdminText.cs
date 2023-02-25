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
        internal const string Requests = "requests";
        internal const string Users = "Users";
        internal const string Settings = "Settings";
        #endregion

        internal readonly static (string, string)[] Menu = { ("Обращения", Requests), ("Пользователи", Users), ("Настройки", Settings) };
        internal const string Good = "Хорошо";
    }
}
