using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("RoomMessages")]
    public class RoomMessage : IEntity
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public long ChatId { get; set; }
        public DateTime DateTime { get; set; }
        public int RoomId { get; set; }
        public string? Text { get; set; }
        public string? From { get; set; }
    }
}