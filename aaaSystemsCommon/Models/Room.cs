using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("Room")]
    public class Room : IEntity
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public long ClientId { get; set; }
        public List<RoomMessage>? RoomMessages { get; set; }
        public List<Participant>? Participants { get; set; }
    }
}
