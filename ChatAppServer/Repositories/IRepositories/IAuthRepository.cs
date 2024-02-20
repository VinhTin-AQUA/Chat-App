using ChatAppServer.Models;
using Microsoft.AspNetCore.Identity;

namespace ChatAppServer.Repositories.IRepositories
{
    public interface IAuthRepository
    {
        Task<IdentityResult> CreateUserAsync(AppUser user, string password);
        Task<AppUser> GetUserByEmail(string email);
        List<AppUser> GetAllUsers();
        Task<SignInResult> LoginAsync(AppUser user, string password);

        AppUser GetUserByUniqueCode(string uniqueCodeUser);
        
    }
}
