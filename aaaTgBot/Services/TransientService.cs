using aaaSystemsCommon.Services.CrudServices;
using System.Net;

namespace aaaTgBot.Services
{
    public static class TransientService
    {
        public static UsersService GetUsersService() => new(AppSettings.BackRoot, GetClient());

        public static HttpClient GetClient()
        {
            if (!string.IsNullOrWhiteSpace(AppSettings.ApiToken)) //TODO
            {
                HttpClientHandler handler = new()
                {
                    CookieContainer = new CookieContainer()
                };
                handler.CookieContainer.Add(new Uri(AppSettings.BackRoot), new Cookie("token", AppSettings.ApiToken));
                return new HttpClient(handler);
            }
            return new HttpClient();
        }
    }
}
