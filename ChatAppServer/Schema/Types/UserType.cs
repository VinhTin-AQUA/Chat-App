using ChatAppServer.Schema.Types.IType;

namespace ChatAppServer.Schema.Types
{
    public class UserType : IResultType
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public List<string> FriendIds { get; set; } = [];

        public List<string> GroupIds { get; set; } = [];
        public string Token { get; set; } = string.Empty;

    }
}
