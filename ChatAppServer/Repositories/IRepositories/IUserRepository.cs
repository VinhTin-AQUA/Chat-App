using ChatAppServer.Models;

namespace ChatAppServer.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<List<AppUser>> SearchUsersByName(string name);
    }
}
