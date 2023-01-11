using System.Runtime.Serialization;

namespace aaaTgBot.Data.Exceptions
{
    [Serializable]
    internal class UserNotFound : Exception
    {
        public long ChatIid { get; private set; }
        public long Id { get; private set; }

        public UserNotFound()
        {
        }

        public UserNotFound(long ChatIid)
        {
            this.ChatIid = ChatIid;
        }

        public UserNotFound(int id)
        {
            this.Id = id;
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
