namespace ChatAppServer.Schema.Types
{
    public class GroupType
    {
        public string Id { get; set; } = string.Empty;

        public string GroupName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public List<string> MemberIds { get; set; } = [];
        public List<string> MessageIds { get; set; } = [];
    }
}
