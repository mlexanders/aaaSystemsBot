using aaaSystemsCommon.Models;

namespace aaaSystemsApi.Repository
{
    public class RoomRepository : BaseCrudRepository<Room, int>
    {
        public RoomRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
