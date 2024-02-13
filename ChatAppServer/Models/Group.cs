using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;

namespace ChatAppServer.Models
{
    [CollectionName("groups")]
    public class Group
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public List<string> MemberIds { get; set; } = [];
        public List<string> MessageIds { get; set; } = [];
    }
}
