using ChatAppServer.Models;

namespace ChatAppServer.Repositories.IRepositories
{
    public interface IGroupRepository
    {
        Task CreateGroupToUser(Group group);
        Task<List<Group>> GetGroupsOfUser(string ownerId);
    }
}
