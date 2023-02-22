namespace aaaSystems.Bot.Features.Client
{
    internal static class ClientText
    {
        internal const string StartMessage = "Стартовое сообщение для клиента";
        internal const string InfoMessage = "Напишите нам. Все отправленные сообщения будут доставлены в поддержку";
    }

    internal static class ClientCallback
    {
        #region Menu
        /// <summary>
        /// Оставить заявку
        /// </summary>
        internal const string WriteRequest = "writeRequest";
        /// <summary>
        /// Написать
        /// </summary>
        internal const string Write = "write";
        #endregion

        internal readonly static (string, string)[] Menu = { ("Оставить заявку", WriteRequest), ("Написать", Write) };
        internal const string Good = "Хорошо";
    }
}
