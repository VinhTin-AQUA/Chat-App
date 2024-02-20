using ChatAppServer.Data;
using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatAppServer.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IMongoCollection<Group> groupCollection;

        public GroupRepository(IOptions<MongoSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionUri);
            var database = client.GetDatabase(options.Value.DatabaseName);
            groupCollection = database.GetCollection<Group>(options.Value.GroupCollectionName);
        }

        public async Task CreateGroupToUser(Group group)
        {
            await groupCollection.InsertOneAsync(group);
        }

        public async Task<List<Group>> GetGroupsOfUser(string ownerId)
        {
            var filter = Builders<Group>.Filter.Eq("OwnerId", ownerId);
            var groups = await groupCollection.Find(filter).ToListAsync();
            return groups;
        }
    }
}
