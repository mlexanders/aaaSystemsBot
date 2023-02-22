namespace aaaSystems.Bot.Features.Administrator
{
    internal static class AdminText
    {
        public const string Start   = "Стартовое сообщение";
        public const string Menu    = "Меню";
        public const string AllRequestsMessage = "Тут все обращения клиентов";
        public const string AllUsers = "Список всех пользователей";
    }

    internal static class AdminCallback
    {

        #region Menu
        public const string requests = "requests";
        public const string users = "users";
        public const string settings = "settings";
        #endregion

        public readonly static (string, string)[] Menu = { ("Обращения", requests), ("Пользователи", users), ("Настройки", settings)};
        public const string Good = "Хорошо";
    }
}
