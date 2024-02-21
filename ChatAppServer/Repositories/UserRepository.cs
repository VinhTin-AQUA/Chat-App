using ChatAppServer.Data;
using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatAppServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<AppUser> userCollection;
        private readonly IMongoCollection<Group> groupCollection;

        public UserRepository(IOptions<MongoSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionUri);
            var database = client.GetDatabase(options.Value.DatabaseName);
            userCollection = database.GetCollection<AppUser>(options.Value.UserCollectionName);
            groupCollection = database.GetCollection<Group>(options.Value.GroupCollectionName);
        }

        public async Task AddGroupUniqueCodeToUser(string email, string uniqueCodeGroup)
        {
            var filter = Builders<AppUser>.Filter.Eq("Email", email);
            var update = Builders<AppUser>.Update.AddToSet("GroupUniqueCodes", uniqueCodeGroup);
            await userCollection.UpdateOneAsync(filter, update);
        }

        public async Task AddUserToGroup(string uniqueCodeUser, string uniqueCodeGroup)
        {
            // tim user
            var filter = Builders<AppUser>.Filter.Eq("UniqueCodeUser", uniqueCodeUser);

            // kiểm tra user có tồn tại không
            var user = await userCollection.Find(filter).FirstOrDefaultAsync();
            if (user == null)
            {
                return;
            }

            // kiểm tra group đã có chưa
            if (user.GroupUniqueCodes.Contains(uniqueCodeUser))
            {
                return;
            }

            // cập nhật danh sách member của group
            var filterGroup = Builders<Group>.Filter.Eq("UniqueCodeGroup", uniqueCodeGroup);
            var updateGroup = Builders<Group>.Update.AddToSet("Members", uniqueCodeUser);
            await groupCollection.UpdateOneAsync(filterGroup, updateGroup);

            // cập nhật danh sách group của một user
            var update = Builders<AppUser>.Update.AddToSet("GroupUniqueCodes", uniqueCodeGroup);
            await userCollection.UpdateOneAsync(filter, update);
        }
    }
}
