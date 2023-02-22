using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class RoomMessagesService : BaseCRUDService<RoomMessage, int>, IRoomMessage
    {
        public RoomMessagesService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }
    }
}
