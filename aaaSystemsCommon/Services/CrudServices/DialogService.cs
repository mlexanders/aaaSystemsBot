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
    }
}
