using ChatAppServer.Data;
using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class MessageQuery
    {
        private readonly IMongoCollection<AppUser> userCollection;

        public MessageQuery(IOptions<MongoSetting> options)
        {
            var client = new MongoClient(options.Value.ConnectionUri);
            var database = client.GetDatabase(options.Value.DatabaseName);
            userCollection = database.GetCollection<AppUser>(options.Value.UserCollectionName);
        }

        public async Task<List<MessageType>> GetMessagesOfGroup(string uniqueCodeGroup, [Service] IMessageRepository messageRepository)
        {
            var messages = await messageRepository.GetMessagesOfGroup(uniqueCodeGroup);
            return messages.Select(m => new MessageType
            {
               SenderId = m.SenderId,
               SenderName = m.SenderName,
               AvatarSender = m.AvatarSender,
               Content = m.Content,
               TimeStamp = m.TimeStamp,
            }).ToList();
        }
    }
}
