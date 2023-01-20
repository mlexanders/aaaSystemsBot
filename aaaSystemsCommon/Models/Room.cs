using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("Rooms")]
    public class Room : IEntity<int>
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public List<RoomMessage>? RoomMessages { get; set; } = new();
    }
}
