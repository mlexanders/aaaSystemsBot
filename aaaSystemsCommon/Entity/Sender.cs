using aaaSystemsCommon.Difinitions;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;
using System.Runtime.InteropServices;

namespace aaaSystemsCommon.Entity
{
    [EntityRoot("Senders")]
    public class Sender : Entity<long>
    {
        public long ChatId { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public Role Role { get; set; }
        public override long PK { get => ChatId; set => ChatId = value; }
    }
}
