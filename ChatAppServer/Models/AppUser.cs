using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace ChatAppServer.Models
{
    [CollectionName("users")]
    public class AppUser : MongoIdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;

        public List<string> FriendIds { get; set; } = [];

        public List<string> GroupIds { get; set; } = [];

    }
}
