using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class UsersService : BaseCRUDService<User, long>, IUser
    {
        public UsersService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }

        public async Task<List<User>> Admins()
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/Admins");
            return await Deserialize<List<User>>(httpResponse);
        }
    }
}
