using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("Participants")]
    public class Participant : IEntity
    {
        public int Id { get; set; }
        public long UserChatId { get; set; }
        //public int UserId { get; set; }
        public int RoomId { get; set; }
    }
}