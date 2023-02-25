using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class DialogsService : BaseCRUDService<Dialog, long>, IDialog
    {
        public DialogsService(string backRoot, HttpClient httpClient, string entityRoot = null!) : base(backRoot, httpClient, entityRoot)
        {
        }

        public async Task<Dialog> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync(Root + "/GetByChatId/" + chatId);
            return await Deserialize<Dialog>(httpResponse);
        }
    }
}
