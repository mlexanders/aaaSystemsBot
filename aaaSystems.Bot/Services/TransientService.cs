using aaaSystemsCommon.Services.CrudServices;
using System.Net;

namespace aaaSystems.Bot.Services
{
    public static class TransientService
    {
        public static SendersService GetSendersService() => new(AppSettings.BackRoot, GetClient());
        public static DialogsService DialogsService => new(AppSettings.BackRoot, GetClient());
        public static DialogMessagesService DialogMessagesService=> new(AppSettings.BackRoot, GetClient());

        public static HttpClient GetClient()
        {
            if (!string.IsNullOrWhiteSpace(AppSettings.ApiToken)) //TODO : auth
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
