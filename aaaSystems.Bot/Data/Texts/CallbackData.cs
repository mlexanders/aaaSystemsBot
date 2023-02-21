namespace aaaSystems.Bot.Data.Texts
{
    public static class CallbackData
    {
        public const string SendMessagesRoom = "SendMessagesRoom";
        public const string JoinToRoom = "JoinToRoom";

        public static string GetJoinToRoom(long chatId) => $"{JoinToRoom}:{chatId}";
        public static string GetSendMessagesRoom(long chatId) => $"{SendMessagesRoom}:{chatId}";
    }
}
