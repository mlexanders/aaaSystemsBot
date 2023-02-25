using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Entity
{
    [EntityRoot("DialogMessages")]
    public class DialogMessage : Entity<int>
    {
        public int MessageId { get; set; }
        public DateTime DateTime { get; set; }
        public long ChatId { get; set; }
        public override int PK { get => MessageId; set => MessageId = value; }
    }
}
