using ChatAppServer.Models;

namespace ChatAppServer.Repositories.IRepositories
{
    public interface IMessageRepository
    {
        Task SendMessage(Message mess, string uniqueCodeGroup);

        Task<List<Message>> GetMessagesOfGroup(string uniqueCodeGroup);
    }
}
