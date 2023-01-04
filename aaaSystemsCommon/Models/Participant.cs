using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("Participant")]
    public class Participant : IEntity
    {
        public int Id { get; set; }
        public long UserID { get; set; }
        public int RoomId { get; set; }
    }
}