﻿using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models.Difinitions;

namespace aaaSystemsCommon.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public long ChatId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Additional { get; set; }
        public Role Role{ get; set; }
    }
}
