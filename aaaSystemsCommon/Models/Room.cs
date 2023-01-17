using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("Rooms")]
    public class Room : IEntity
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public int ClientId { get; set; }
        public List<RoomMessage>? RoomMessages { get; set; } = new();
    }
}
