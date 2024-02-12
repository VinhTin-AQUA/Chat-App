using ChatAppServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class GroupQuery
    {
        private readonly IMongoCollection<Group> groupCollection;

        public GroupQuery(IOptions<MongoDbSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionUri);
            var database = client.GetDatabase(options.Value.DatabaseName);
            groupCollection = database.GetCollection<Group>(options.Value.GroupCollectionName);
        }
    }
}
