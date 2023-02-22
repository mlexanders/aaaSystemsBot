using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models.Difinitions;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Models
{
    [EntityRoot("Sender")]
    public class Sender : IEntity<long>
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public Role Role { get; set; }
    }
}
