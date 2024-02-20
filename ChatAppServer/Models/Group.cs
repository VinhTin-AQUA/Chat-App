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
        public string OwnerId { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string UniqueCodeGroup {  get; set; } = string.Empty;
        public List<Message> Messages { get; set; } = [];
    }
}
