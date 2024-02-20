using ChatAppServer.Models;

namespace ChatAppServer.Repositories.IRepositories
{
    public interface IUserRepository
    {
       Task AddGroupUniqueCodeToUser(string email, string uniqueCodeGroup);
        Task AddUserToGroup(string uniqueCodeUser, string uniqueCodeGroup);
    }
}
