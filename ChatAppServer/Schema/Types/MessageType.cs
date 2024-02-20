namespace ChatAppServer.Schema.Types
{
    public class MessageType
    {
        public string Id { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string AvatarSender { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
    }
}
