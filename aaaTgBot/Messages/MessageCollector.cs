using aaaSystemsCommon.Utils;
using aaaTgBot.Data;
using aaaTgBot.Services;

namespace aaaTgBot.Messages
{
    public class MessageCollector : MessageCollectorBase
    {
        private readonly int messageId;

        public MessageCollector(long chatId, int messageId) : base(chatId)
        {
            this.messageId = messageId;
        }

        //TODO : EditMessage

        public async Task DeleteMessage()
        {
            await botService.DeleteMessage(messageId);
        }

        public async Task GoToRoom(long chatId, long chatIdClient)
        {
            var roomService = TransientService.GetRoomsService();
            var room = await roomService.GetByChatId(chatIdClient);

            foreach (var msg in room.RoomMessages)
            {
                await botService.Forward(chatId, chatIdClient, msg.MessageId);
            }
        }
    }
}