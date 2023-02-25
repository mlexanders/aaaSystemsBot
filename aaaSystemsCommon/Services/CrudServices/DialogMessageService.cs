using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Services.Base;
using aaaSystemsCommon.Utils;
using System.Text;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class DialogMessagesService : BaseCRUDService<DialogMessage, int>, IDialogMessage
    {
        public DialogMessagesService(string backRoot, HttpClient httpClient, string entityRoot = null!) : base(backRoot, httpClient, entityRoot)
        {
        }

        public async Task PostByChatId(long chatId, int messageId)
        {
            var json = Serialize(messageId);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync($"{Root}/PostByChatId/{chatId}", data);
            if (!httpResponse.IsSuccessStatusCode) throw new ErrorResponseException(httpResponse.StatusCode, await httpResponse.Content.ReadAsStringAsync());
        }
    }
}
