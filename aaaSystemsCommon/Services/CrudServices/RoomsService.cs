using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class RoomsService : BaseCRUDService<Room>, IRoom
    {
        public RoomsService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }

        public async Task<Room> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root + "/GetByChatId" + chatId);
            return await Deserialize<Room>(httpResponse);
        }
    }
}
