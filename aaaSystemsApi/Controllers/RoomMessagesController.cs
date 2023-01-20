using aaaSystemsApi.Repository;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
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
