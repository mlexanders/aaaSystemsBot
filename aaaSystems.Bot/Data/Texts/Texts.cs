namespace aaaSystems.Bot.Data.Texts
{
    public static class Texts
    {
        public const string StartMessage = "Хелова, здесь вы можете подать заявку на обслуживание, а также связаться с поддержкой";
        public const string InfoMessage = "Отправьте сообщение, оно будет доставлено нашему менеджеру";
        public const string SubmitAnApplication = "Задайте вопрос или оставьте заявку";
        public const string UnknownMessage = "Пока я не понимаю, но скоро научусь";
        public const string NoApplications = "Заявок пока нет";
        public const string UnknownUser = "Пользователь не найден";

        public static string InfoMessageForAdmin(string? name) => $"Отправьте сообщение, оно будет доставлено {name ?? "'❓"}";
    }
}
