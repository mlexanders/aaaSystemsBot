namespace aaaSystemsCommon.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public long AdminId { get; set; }
        public long UserId { get; set; }
        public List<Message> Message { get; set; }
    }
}
