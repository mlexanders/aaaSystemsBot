using aaaSystemsApi.Repository;
using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogsController : BaseCrudController<Dialog, long>, IDialog
    {
        public DialogsController(DialogRepository repository) : base(repository)
        {
        }

        [HttpGet("GetByChatId/{chatId}")]
        public Task<Dialog> GetByChatId(long chatId)
        {
            return repository.ReadFirst(d => d.ChatId.Equals(chatId));
        }
    }
}
