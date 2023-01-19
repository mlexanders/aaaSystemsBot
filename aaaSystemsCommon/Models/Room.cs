﻿using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("Rooms")]
    public class Room : IEntity<int>
    {
        public int Id { get; set; }
        public List<RoomMessage>? RoomMessages { get; set; } = new();
        public long UserId { get; set; }
        //public int RoomId { get; set; }
        //public User? User { get; set; }
    }
}
