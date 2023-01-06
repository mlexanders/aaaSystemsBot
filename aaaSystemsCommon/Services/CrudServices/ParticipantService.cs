using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Models;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class ParticipantService : BaseCRUDService<Participant>, IParticipant
    {
        public ParticipantService(string backRoot, HttpClient httpClient, string entityRoot = null) : base(backRoot, httpClient, entityRoot) { }
    }
}
