namespace aaaTgBot.Data
{
    public static class Texts
    {
        public const string StartMessage = "Здравствуйте, добро пожаловать в чат поддержки👋";
        public const string InfoMessage = "Напишите ваш вопрос, менеджер ответит на него в ближайшее время";
        public const string SubmitAnApplication = "Выберите дальнейшее действие";
        public const string UnknownMessage = "Пока я не понимаю, но скоро научусь";
        public const string NoApplications = "Заявок пока нет";
        public const string UnknownUser = "Пользователь не найден";

        public static string InfoMessageForAdmin(string? name) => $"Отправьте сообщение, оно будет доставлено {name ?? "'❓"}";
    }
}
