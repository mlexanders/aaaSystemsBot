using aaaSystemsApi.Repository;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseCrudController<User> 
    {
        private readonly BaseCrudRepository<User> repository;

        public UserController(BaseCrudRepository<User> repository) : base(repository)
        {
            this.repository = repository;
        }

    }
}
