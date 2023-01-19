using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class UsersService : BaseCRUDService<User>, IUser
    {
        public UsersService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }

        public async Task<User> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/GetByChatId/{chatId}");
            return await Deserialize<User>(httpResponse);
        }

        public async Task<List<User>> Admins()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/Admins");
            return await Deserialize<List<User>>(httpResponse);
        }
    }
}
