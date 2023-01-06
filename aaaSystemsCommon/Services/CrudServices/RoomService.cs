using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class RoomService : BaseCRUDService<Room>, IRoom
    {
        public RoomService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }
    }
}
