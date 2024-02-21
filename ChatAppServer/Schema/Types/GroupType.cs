namespace ChatAppServer.Schema.Types
{
    public class GroupType
    {
        public string Id { get; set; } = string.Empty;
        public string OwnerId { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UniqueCodeGroup { get; set; } = string.Empty;
        public List<string> Members { get; set; } = [];
    }
}
