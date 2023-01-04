using aaaSystemsApi.Repository;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseCrudController<User>
    {
        public UserController(BaseCrudRepository<User> repository) : base(repository) { }

        [HttpGet("GetByChatId")]
        public async Task<User> GetByChatId(int chatId)
        {
            return await repository.ReadFirst(u => u.ChatId == chatId);
        }
    }
}
