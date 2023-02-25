using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Entity
{
    [EntityRoot("DialogMessages")]
    public class DialogMessage : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public long ChatId { get; set; }
    }
}
