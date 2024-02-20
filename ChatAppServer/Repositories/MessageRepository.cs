using ChatAppServer.Data;
using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace ChatAppServer.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMongoCollection<Group> groupCollection;

        public MessageRepository(IOptions<MongoSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionUri);
            var database = client.GetDatabase(options.Value.DatabaseName);
            groupCollection = database.GetCollection<Group>(options.Value.GroupCollectionName);
        }

        public async Task SendMessage(Message mess, string uniqueCodeGroup)
        {
            var filter = Builders<Group>.Filter.Eq("UniqueCodeGroup", uniqueCodeGroup);
            var update = Builders<Group>.Update.AddToSet("Messages", mess);
            await groupCollection.UpdateOneAsync(filter, update);
        }

        public async Task<List<Message>> GetMessagesOfGroup(string uniqueCodeGroup)
        {
            var filter = Builders<Group>.Filter.Eq("UniqueCodeGroup", uniqueCodeGroup);
            var group = await groupCollection.Find(filter).FirstOrDefaultAsync();
            return group.Messages;
        }
    }
}
