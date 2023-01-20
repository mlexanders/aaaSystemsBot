using aaaSystemsCommon.Models;

namespace aaaSystemsApi.Repository
{
    public class RoomMessageRepository : BaseCrudRepository<RoomMessage, int>
    {
        public RoomMessageRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
