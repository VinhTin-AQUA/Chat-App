namespace ChatAppServer.Schema.Types
{
    public class NoticeType
    {
        public string ReceiverId { get; set; } = string.Empty;
        public string SenderId { get; set; } = string.Empty;
        public string RelatedName { get; set; } = string.Empty;
    }
}
