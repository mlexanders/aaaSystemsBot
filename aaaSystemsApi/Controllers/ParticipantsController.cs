using aaaSystemsApi.Repository;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantsController : BaseCrudController<Participant>, IParticipant
    {
        public ParticipantsController(BaseCrudRepository<Participant> repository) : base(repository) { }

        [HttpGet("GetByChatId/{chatId}")]
        public async Task<Participant> GetByChatId(long chatId)
        {
            return await repository.ReadFirst(u => u.UserChatId == chatId);
        }
    }
}
