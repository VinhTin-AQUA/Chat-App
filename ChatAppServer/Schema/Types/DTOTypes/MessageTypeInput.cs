namespace ChatAppServer.Schema.Types.DTOTypes
{
    public class MessageTypeInput
    {
        public string Content { get; set; } = string.Empty;
        public string UniqueCodeGroup { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
