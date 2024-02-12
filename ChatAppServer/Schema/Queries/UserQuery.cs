using ChatAppServer.Models;
using ChatAppServer.Schema.Types;
using HotChocolate.Types;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        private readonly IMongoCollection<AppUser> userCollection;

        public UserQuery(IOptions<MongoDbSetting> options)
        {
            MongoClient client = new MongoClient(options.Value.ConnectionUri);
            IMongoDatabase database = client.GetDatabase(options.Value.DatabaseName);
            userCollection = database.GetCollection<AppUser>(options.Value.UserCollectionName);
        }


        public async Task<UserType?> GetUserByEmail(string email)
        {
            var filter = Builders<AppUser>.Filter.Eq("Email", email);
            var user = await userCollection.FindAsync(filter);

            if (user as AppUser == null)
            {
                return null;
            }

            AppUser r = (AppUser)user;
            return new UserType
            {
                Email = r.Email!,
                PhoneNumber = r.PhoneNumber!,
                FullName = r.FullName!,
                AvatarUrl = r.AvatarUrl!,
                FriendIds = r.FriendIds!,
                GroupIds = r.GroupIds!
            };
        }

        public async Task<List<UserType>> GetAllUsers()
        {
            var user = await userCollection.FindAsync(_ => true);
            var userTypes = user.ToList()
                .Select(u => new UserType
                {
                    Email = u.Email!,
                    PhoneNumber = u.PhoneNumber!,
                    FullName = u.FullName,
                    AvatarUrl = u.AvatarUrl,
                    FriendIds = u.FriendIds,
                    GroupIds = u.GroupIds

                }).ToList();
            return userTypes;
        }
    }
}
