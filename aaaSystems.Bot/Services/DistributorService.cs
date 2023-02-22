using aaaSystems.Bot.Data;
using aaaSystems.Bot.Handlers;
using aaaSystemsCommon.Difinitions;
using aaaSystemsCommon.Services.CrudServices;
using Telegram.Bot.Types;

namespace aaaSystems.Bot.Services
{
    internal static class DistributorService
    {
        private static readonly UnAuthorizedHandler unAuthorizedHandler = new();
        private static readonly AdminHandler adminHandler = new();
        private static readonly ClientHandler clientHandler = new();
        private static SendersService SendersService { get => TransientService.GetSendersService(); }

        internal static async Task ProcessUpdate(Update update)
        {
            var sender = await SendersService.Get(update.GetChatId());

            if (sender == null)
            {
                await unAuthorizedHandler.ProcessUpdate(update);
            }
            else if (sender.Role is Role.Admin)
            {
                await adminHandler.ProcessUpdate(update);
            }
            else if (sender.Role is Role.Client)
            {
                await clientHandler.ProcessUpdate(update);
            }
        }
    }
}
