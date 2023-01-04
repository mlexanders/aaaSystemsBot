using aaaSystemsCommon.Interfaces;

namespace aaaSystemsCommon.Models
{
    public class RoomMessage : IEntity
    {
        public int Id { get; set; }

        public long MessageId { get; set; }
        public long UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int RoomId { get; set; }
    }
}