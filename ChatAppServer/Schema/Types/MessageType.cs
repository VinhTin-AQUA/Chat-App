namespace ChatAppServer.Schema.Types
{
    public class MessageType
    {
        public string Id { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string Receiver { get; set; } = string.Empty;
        public string GroupId { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}
