using aaaSystemsCommon.Entity;

namespace aaaSystemsApi.Repository
{
    public class SenderRepository : BaseCrudRepository<Sender, long>
    {
        public SenderRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
