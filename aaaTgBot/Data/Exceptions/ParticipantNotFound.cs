using System.Runtime.Serialization;

namespace aaaTgBot.Data.Exceptions
{
    [Serializable]
    public class ParticipantNotFound : Exception
    {
        public long ChatId { get; }

        public ParticipantNotFound()
        {
        }

        public ParticipantNotFound(string? message) : base(message)
        {
        }

        public ParticipantNotFound(long id)
        {
            ChatId = id;
        }

        public ParticipantNotFound(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ParticipantNotFound(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
