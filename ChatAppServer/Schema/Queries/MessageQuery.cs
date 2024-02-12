using ChatAppServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class MessageQuery
    {
        private readonly IMongoCollection<Message> messageCollection;

        public MessageQuery(IOptions<MongoDbSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionUri);
            var database = client.GetDatabase(options.Value.DatabaseName);
            this.messageCollection = database.GetCollection<Message>(options.Value.MessageCollectionName);
        }
    }
}
