using aaaSystemsApi.Repository;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomMessageController : BaseCrudController<RoomMessage>
    {
        public RoomMessageController(BaseCrudRepository<RoomMessage> repository) : base(repository) { }
    }
}
