using System.ComponentModel.DataAnnotations;

namespace ChatAppServer.Schema.Types.DTOTypes
{
    public class RegisterInputType
    {
        public string Email { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ReEnterPassword { get; set; } = string.Empty;

        public string AvatarUrl { get; set; } = string.Empty;
    }
}
