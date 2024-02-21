using ChatAppServer.Data;
using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ChatAppServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<AppUser> userCollection;

        public UserRepository(IOptions<MongoSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionUri);
            var database = client.GetDatabase(options.Value.DatabaseName);
            userCollection = database.GetCollection<AppUser>(options.Value.UserCollectionName);
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
            
            // kiểm tra group đã có chưa
            var user = await userCollection.Find(filter).FirstOrDefaultAsync();
            if(user.GroupUniqueCodes.Contains(uniqueCodeUser))
            {
                return;
            }

            // thêm group
            var update = Builders<AppUser>.Update.AddToSet("GroupUniqueCodes", uniqueCodeGroup);
            await userCollection.UpdateOneAsync(filter, update);
        }
    }
}
