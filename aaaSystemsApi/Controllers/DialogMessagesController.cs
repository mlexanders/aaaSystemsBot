using aaaSystemsApi.Repository;
using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DialogMessagesController : BaseCrudController<DialogMessage, int>, IDialogMessage
    {
        private readonly DialogRepository dialogRepository;

        public DialogMessagesController(DialogMessageRepository repository, DialogRepository dialogRepository) : base(repository)
        {
            this.dialogRepository = dialogRepository;
        }

        [HttpGet("GetByDialogId/{dialogId}")]
        public Task<List<DialogMessage>> GetByDialogId(long dialogId)
        {
            return repository.Read(m => m.DialogId.Equals(dialogId));
        }

        [HttpPost("PostByChatId/{chatId}")]
        public virtual async Task PostByChatId(long chatId, [FromBody] int messageId)
        {
            var dialog = await dialogRepository.ReadFirst(d => d.ChatId.Equals(chatId)) ?? throw new();

            await repository.Create(new DialogMessage()
            {
                Id = messageId,
                DialogId = dialog.Id
            });
        }
    }
}
