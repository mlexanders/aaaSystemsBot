using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class ParticipantsService : BaseCRUDService<Participant>, IParticipant
    {
        public ParticipantsService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }

        public async Task<Participant> GetByChatId(long chatId)
        {
            HttpResponseMessage httpResponse = await httpClient.GetAsync($"{Root}/GetByChatId/{chatId}");
            return await Deserialize<Participant>(httpResponse);
        }
    }
}
