using aaaSystems.Bot.Data.Interfaces;
using aaaSystems.Bot.Features.Administrator;
using aaaSystems.Bot.Handlers;
using aaaSystemsCommon.Entity;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotLib;

namespace aaaSystems.Bot.Services
{
    public class DialogNotificationService : IDialogNotificationService
    {
        private readonly DialogHandler _handler;
        private readonly TelegramBotClient _bot;

        public DialogNotificationService(DialogHandler specialHandler)
        {
            _handler = specialHandler;
            _bot = BotClient.Client;
        }

        public async Task NotificateAdministrators(Message message)
        {
            var IdAndHandler = GetHandlingIds();

            var senders = TransientService.GetSendersService();
            var senderT = senders.Get(message.Chat.Id);
            var allAdminsIds = (await senders.Admins()).Select(s => s.Id);

            foreach (var adminId in allAdminsIds)
            {
                if (IdAndHandler.Contains(adminId))
                {
                    var admin = await _handler.participants.Get(adminId);
                    await admin.Notificate(message);
                }
                else
                {
                    await SendNotificationMessage(adminId, message, await senderT);
                }
            }
        }

        private Task SendNotificationMessage(long adminId, Message message, Sender sender) // TODO : переделать
        {
            var buttons = new ButtonsGenerator();
            buttons.SetInlineButtons(AdminCallback.GetNotificationMenu(message.Chat.Id));

            return _bot.SendTextMessageAsync(adminId, $"От {sender.Name} новое сообщение", replyMarkup: buttons.GetButtons());
        }

        private IEnumerable<long> GetHandlingIds()
        {
            return UpdateHandler.HandlingSenders.Where(d => d.Value.Equals(_handler)).Select(a => a.Key);
        }
    }
}
