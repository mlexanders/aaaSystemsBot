using aaaSystemsApi.Repository;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseCrudController<User>, IUser
    {
        public UserController(BaseCrudRepository<User> repository) : base(repository) { }

        [HttpGet("GetByChatId")]
        public async Task<User> GetByChatId(long chatId)
        {
            return await repository.ReadFirst(u => u.ChatId == chatId);
        }
    }
}
