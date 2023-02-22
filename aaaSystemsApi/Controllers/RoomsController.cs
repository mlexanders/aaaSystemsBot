using aaaSystemsApi.Repository;
using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : BaseCrudController<Room, int>, IRoom
    {
        public RoomsController(RoomRepository repository) : base(repository) { }

        public override async Task<List<Room>> Get()
        {
            return await repository.Read(c => c.RoomMessages != null, m => m.RoomMessages);
        }

        public override async Task<Room> Get(int id)
        {
            return await repository.ReadFirst(c => c.Id == id, m => m.RoomMessages);
        }

        [HttpGet("GetByChatId{chatId}")]
        public async Task<Room> GetByChatId(long chatId)
        {
            return await repository.ReadFirst(r => r.UserId == chatId, m => m.RoomMessages);
        }
    }
}
