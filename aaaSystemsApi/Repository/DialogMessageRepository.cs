using aaaSystemsCommon.Entity;

namespace aaaSystemsApi.Repository
{
    public class DialogMessageRepository : BaseCrudRepository<DialogMessage, int>
    {
        public DialogMessageRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
