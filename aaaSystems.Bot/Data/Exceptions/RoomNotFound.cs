using System.Runtime.Serialization;

namespace aaaSystems.Bot.Data.Exceptions
{
    [Serializable]
    internal class RoomNotFound : Exception
    {
        public RoomNotFound()
        {
        }

        public RoomNotFound(string? message) : base(message)
        {
        }

        public RoomNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RoomNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
