using aaaSystemsApi.Repository;
using aaaSystemsCommon.Difinitions;
using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendersController : BaseCrudController<Sender, long>, ISender
    {
        public SendersController(SenderRepository repository) : base(repository) { }

        [HttpGet("Admins")]
        public async Task<List<Sender>> Admins()
        {
            return await repository.Read(u => u.Role == Role.Admin);
        }
    }
}
