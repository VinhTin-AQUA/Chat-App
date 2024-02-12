using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace ChatAppServer.Models
{
    [CollectionName("roles")]
    public class AppRole : MongoIdentityRole<Guid>
    {
    }
}
