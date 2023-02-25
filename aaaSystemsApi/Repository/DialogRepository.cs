using aaaSystemsCommon.Entity;

namespace aaaSystemsApi.Repository
{
    public class DialogRepository : BaseCrudRepository<Dialog, long>
    {
        public DialogRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
