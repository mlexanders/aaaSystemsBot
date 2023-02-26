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

        public override Task<Dialog> Get(long id)
        {
            return repository.ReadFirst(d => d.Id.Equals(id), d => d.Sender!);
        }

        public override Task<List<Dialog>> Get()
        {
            return repository.Read(includedProperties: d => d.Sender!);
        }

        [HttpGet("GetByChatId/{chatId}")]
        public Task<Dialog> GetByChatId(long chatId)
        {
            return repository.ReadFirst(d => d.ChatId.Equals(chatId), d => d.Sender!);
        }
    }
}
