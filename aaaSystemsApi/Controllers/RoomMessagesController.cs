using aaaSystemsApi.Repository;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomMessagesController : BaseCrudController<RoomMessage>, IRoomMessage
    {
        public RoomMessagesController(BaseCrudRepository<RoomMessage> repository) : base(repository) { }
    }
}
