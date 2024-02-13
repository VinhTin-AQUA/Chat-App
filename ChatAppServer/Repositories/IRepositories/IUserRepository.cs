using ChatAppServer.Models;

namespace ChatAppServer.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(AppUser user, string password);
    }
}
