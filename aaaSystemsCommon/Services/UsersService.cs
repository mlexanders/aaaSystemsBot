using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;

namespace aaaSystemsCommon.Services
{
    public class UsersService : BaseCRUDService<User>, IUser
    {
        public UsersService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }

        public async Task<User> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/GetByChatId/{chatId}");
            return await Deserialize<User>(httpResponse);
        }
    }
}
