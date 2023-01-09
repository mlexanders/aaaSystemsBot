using System.Runtime.Serialization;

namespace aaaTgBot.Data.Exceptions
{
    [Serializable]
    internal class UserNotFound : Exception
    {
        private long id;

        public UserNotFound()
        {
        }

        public UserNotFound(long id)
        {
            this.id = id;
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
