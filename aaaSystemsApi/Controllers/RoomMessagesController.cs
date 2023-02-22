using aaaSystemsApi.Repository;
using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomMessagesController : BaseCrudController<RoomMessage, int>, IRoomMessage
    {
        public RoomMessagesController(RoomMessageRepository repository) : base(repository) { }
    }
}
