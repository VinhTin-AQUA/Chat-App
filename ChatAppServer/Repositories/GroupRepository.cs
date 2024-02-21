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
        private readonly IMongoCollection<AppUser> userCollection;

        public GroupRepository(IOptions<MongoSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionUri);
            var database = client.GetDatabase(options.Value.DatabaseName);
            groupCollection = database.GetCollection<Group>(options.Value.GroupCollectionName);
            userCollection = database.GetCollection<AppUser>(options.Value.UserCollectionName);
        }

        public async Task CreateGroupToUser(Group group)
        {
            await groupCollection.InsertOneAsync(group);
        }

        public async Task<List<Group>> GetGroupsOfUser(string ownerId)
        {
            var user = await userCollection.Find(u => u.UniqueCodeUser ==  ownerId).FirstOrDefaultAsync();
            


            var groups = await groupCollection.Find(g => user.GroupUniqueCodes.Contains(g.UniqueCodeGroup)).ToListAsync();
            return groups;
        }

        public async Task<Group> GetGroup(string uniqueCodeGroup)
        {
            var filter = Builders<Group>.Filter.Eq("UniqueCodeGroup", uniqueCodeGroup);
            var group = await groupCollection.Find(filter).FirstOrDefaultAsync();
            return group;
        }
    }
}
