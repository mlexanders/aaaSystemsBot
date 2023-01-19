using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;
using System.ComponentModel.DataAnnotations;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("RoomMessages")]
    public class RoomMessage : IEntity<int>
    {
        public int Id { get; set; } // messageID
        public DateTime DateTime { get; set; }
        public string? Text { get; set; }
        public string? From { get; set; }
        public long UserId { get; set; }
        //public User? User { get; set; }
    }
}