using aaaSystemsApi.Repository;
using aaaSystemsCommon.Models;
using Microsoft.AspNetCore.Mvc;

namespace aaaSystemsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : BaseCrudController<Chat>
    {
        public ChatController(BaseCrudRepository<Chat> repository) : base(repository) { }

        [HttpPost("Test")]
        public async Task Test()
        {
            var chat = new Chat()
            {
                ChatId = 1,
                AdminId = 1,
                UserId = 1,
                Message = new List<Message>()
                {
                    new Message()
                    {
                        MessageId = 1
                    },
                    new Message()
                    {
                        MessageId = 2
                    }
                }
            };
            await repository.Create(chat);
        }

        public override async Task<List<Chat>> Get()
        {
            return await repository.Read(c => c.Message != null, m => m.Message);
        }

        public override async Task<Chat> Get(int id)
        {
            return await repository.ReadFirst(c => c.Id == id && c.Message != null, m => m.Message);
        } 
    }
}
