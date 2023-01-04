using aaaSystemsApi.Repository;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : BaseCrudController<Room>
    {
        public RoomController(BaseCrudRepository<Room> repository) : base(repository) { }

        public override async Task<List<Room>> Get()
        {
            return await repository.Read(c => c.RoomMessages != null, m => m.RoomMessages, p => p.Participants);
        }

        public override async Task<Room> Get(int id)
        {
            return await repository.ReadFirst(c => c.Id == id, m => m.RoomMessages, p => p.Participants);
        }
        // return await repository.ReadFirst(c => c.Id == id, m => m.RoomMessages != null ? m.RoomMessages : null, p => p.Participants != null);
    }
}
