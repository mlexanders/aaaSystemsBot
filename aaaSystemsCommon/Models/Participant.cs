using aaaSystemsCommon.Interfaces;

namespace aaaSystemsCommon.Models
{
    public class Participant : IEntity
    {
        public int Id { get; set; }
        public long UserID { get; set; }
        public int RoomId { get; set; }
    }
}