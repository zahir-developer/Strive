namespace Greeter.DTOs
{
    public class ChatInfo
    {
        public string Title { get; set; }
        public long GroupId { get; set; }
        public long SenderId { get; set; }
        public long RecipientId { get; set; }
        public string CommunicationId { get; set; }
    }
}
