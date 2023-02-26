using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Utils;

namespace aaaSystemsCommon.Entity
{
    [EntityRoot("Dialogs")]
    public class Dialog : IEntity<long>
    {
        public long Id { get; set; }
        public bool IsNeedAnswer { get; set; }

        public long ChatId { get; set; }
        public Sender? Sender { get; set; }
    }
}
