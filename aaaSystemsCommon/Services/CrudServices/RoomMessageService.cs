using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class RoomMessageService : BaseCRUDService<RoomMessage>, IRoomMessage
    {
        public RoomMessageService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }
    }
}
