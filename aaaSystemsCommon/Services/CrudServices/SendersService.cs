using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class SendersService : BaseCRUDService<Sender, long>, ISender
    {
        public SendersService(string backRoot, HttpClient httpClient, string entityRoot = null!) : base(backRoot, httpClient, entityRoot) { }

        public async Task<List<Sender>> Admins()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/Admins");
            return await Deserialize<List<Sender>>(httpResponse);
        }
    }
}
