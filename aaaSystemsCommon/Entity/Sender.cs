using aaaSystemsCommon.Difinitions;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Entity
{
    [EntityRoot("Senders")]
    public class Sender : IEntity<long>
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public Role Role { get; set; }
    }
}
