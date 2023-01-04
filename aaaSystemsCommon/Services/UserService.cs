using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;

namespace aaaSystemsCommon.Services
{
    public class UserService : BaseCRUDService<User>, IUser
    {
        public UserService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }

        public async Task<User> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/ByChatId/{chatId}");
            return await Deserialize<User>(httpResponse);
        }
    }
}
