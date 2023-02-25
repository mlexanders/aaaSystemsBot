using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Entity
{
    [EntityRoot("Dialogs")]
    public class Dialog : Entity<long>
    {
        public long ChatId { get; set; }
        public bool IsNeedAnswer { get; set; }
        public override long PK { get => ChatId; set => ChatId = value; }
    }
}
