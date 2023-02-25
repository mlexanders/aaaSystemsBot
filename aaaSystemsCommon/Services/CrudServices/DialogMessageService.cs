using aaaSystemsCommon.Entity;
using aaaSystemsCommon.Interfaces;
using aaaSystemsCommon.Services.Base;

namespace aaaSystemsCommon.Services.CrudServices
{
    public class DialogMessagesService : BaseCRUDService<DialogMessage, int>, IDialogMessage
    {
        public DialogMessagesService(string backRoot, HttpClient httpClient, string entityRoot = null!) : base(backRoot, httpClient, entityRoot)
        {
        }
    }
}
