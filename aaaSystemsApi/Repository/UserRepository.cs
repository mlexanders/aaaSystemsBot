using aaaSystemsCommon.Models;

namespace aaaSystemsApi.Repository
{
    public class UserRepository : BaseCrudRepository<User, long>
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
