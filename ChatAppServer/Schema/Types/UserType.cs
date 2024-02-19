using ChatAppServer.Schema.Types.IType;

namespace ChatAppServer.Schema.Types
{
    public class UserType : IResultType
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string UniqueCodeUser { get; set; } = string.Empty;
    }
}
