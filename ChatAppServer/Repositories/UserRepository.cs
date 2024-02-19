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

 
    }
}
