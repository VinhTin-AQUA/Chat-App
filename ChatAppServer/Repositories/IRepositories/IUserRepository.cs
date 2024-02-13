using ChatAppServer.Models;
using Microsoft.AspNetCore.Identity;

namespace ChatAppServer.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<AppUser> GetUserByEmail(string email);
        List<AppUser> GetAllUsers();
    }
}
