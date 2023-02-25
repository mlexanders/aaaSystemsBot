using System.Runtime.Serialization;

namespace aaaSystems.Bot.Data.Exceptions
{
    [Serializable]
    internal class MessageNotFound : Exception
    {
        public MessageNotFound()
        {
        }

        public MessageNotFound(string? message) : base(message)
        {
        }

        public MessageNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MessageNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}