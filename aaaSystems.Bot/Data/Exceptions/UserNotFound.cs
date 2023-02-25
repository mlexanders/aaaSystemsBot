using System.Runtime.Serialization;

namespace aaaSystems.Bot.Data.Exceptions
{
    [Serializable]
    internal class UserNotFound : Exception
    {
        public long ChatId { get; private set; }
        public long Id { get; private set; }

        public UserNotFound()
        {
        }

        public UserNotFound(long ChatId)
        {
            this.ChatId = ChatId;
        }

        public UserNotFound(int id)
        {
            Id = id;
        }

        public UserNotFound(string? message) : base(message)
        {
        }

        public UserNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
