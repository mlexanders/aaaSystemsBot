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
        public DialogMessagesController(DialogMessageRepository repository) : base(repository) { }
    }
}
