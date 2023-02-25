using aaaSystems.Bot.Data.Interfaces;
using aaaSystems.Bot.Services;
using aaaSystemsCommon.Entity;
using Telegram.Bot.Types;
using TelegramBotLib.Handlers;
using TelegramBotLib.Interfaces;

namespace aaaSystems.Bot.Handlers
{
    public class DialogHandler : BaseHandler, IBaseSpecialHandler
    {
        public Client Initiator { get; private set; } = null!;
        public Dialog CurrentDialog { get; private set; }

        public readonly IParticipantFactory participants;
        private readonly long chatId;

        public DialogHandler(long chatId)
        {
            this.chatId = chatId;
            participants = new ParticipantFactory(this);
        }

        public async Task StartProcessing()
        {
            Initiator = await participants.GetClient(chatId);
            await TrySetDialog();
            await Initiator.Notificate("infoMessage");
        }

        private async Task TrySetDialog()
        {
            var dialogs = TransientService.GetDialogsService();
            try
            {
                CurrentDialog = await dialogs.GetByChatId(chatId);
            }
            catch
            {
                await dialogs.Post(new Dialog() { ChatId = chatId });
                CurrentDialog = await dialogs.GetByChatId(chatId);
            }
        }

        public override Task ProcessUpdate(Update update)
        {
            return base.ProcessUpdate(update);
        }

        protected override Task ProcessCallbackQuery(CallbackQuery callbackQuery)
        {
            return Task.CompletedTask; //ignore all callback
        }

        protected override async Task ProcessMessage(Message message)
        {
            var participant = await participants.Get(message.Chat.Id);

            await participant.Handle(message);
            await PostMessage(message);
        }

        private Task PostMessage(Message message)
        {
            var dialogMessages = TransientService.GetDialogMessagesService();
            return dialogMessages.PostByChatId(chatId, message.MessageId);
        }
    }

    public abstract class Participant
    {
        public readonly long chatId;
        protected DialogHandler handler;
        protected readonly BotService bot;

        public Participant(long chatId, DialogHandler handler)
        {
            bot = new(chatId);
            this.chatId = chatId;
            this.handler = handler;
        }

        public abstract Task Handle(Message message);

        public virtual Task Add()
        {
            _ = UpdateHandler.HandlingSenders.TryAdd(chatId, handler);
            handler.StartProcessing();
            return Task.CompletedTask;
        }

        public virtual Task Remove()
        {
            _ = UpdateHandler.HandlingSenders.Remove(chatId);
            return Task.CompletedTask;
        }

        public virtual Task Notificate(Message message)
        {
            return bot.SendMessage(message.Text!);
        }
    }

    public class Admin : Participant
    {
        public Admin(long chatId, DialogHandler handler) : base(chatId, handler)
        {
        }

        public override Task Add()
        {
            return base.Add();
        }

        public override async Task Handle(Message message)
        {
            //отправка сообщения клиенту
            await handler.Initiator.Notificate(message);
        }

        public override Task Notificate(Message message)
        {
            // пересылка сообщения от клиента админу
            return bot.Forward(message.Chat.Id, message.MessageId);
        }

        public override Task Remove()
        {
            return base.Remove();
        }
    }

    public class Client : Participant
    {
        public Sender Sender { get; private set; }
        private readonly IDialogNotificationService _notificationService;

        public Client(Sender sender, DialogHandler handler) : base(sender.Id, handler)
        {
            Sender = sender;
            _notificationService = new DialogNotificationService(handler);
        }

        public override Task Add()
        {
            return base.Add();
        }

        public override async Task Handle(Message message)
        {
            // Уведомление администраторов о новом сообщениии
            await _notificationService.NotificateAdministrators(message);
        }

        /// <summary>
        /// Принимает сообщение от админитсратора и отправляет его
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override async Task Notificate(Message message)
        {
            await bot.SendMessage(message.Text!);
        }

        public async Task Notificate(string message)
        {
            await bot.SendMessage(message);
        }

        public override Task Remove()
        {
            return base.Remove();
        }
    }
}
