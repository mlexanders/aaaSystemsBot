using aaaSystemsApi.Repository;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using aaaSystemsCommon.Models.Difinitions;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseCrudController<User, long>, IUser
    {
        public UsersController(UserRepository repository) : base(repository) { }

        [HttpGet("Admins")]
        public async Task<List<User>> Admins()
        {
            return await repository.Read(u => u.Role == Role.Admin);
        }
    }
}
