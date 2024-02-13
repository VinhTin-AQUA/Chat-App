using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace ChatAppServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
        {
            var r = await userManager.CreateAsync(user, password);
            return r;
        }

        public async Task<AppUser> GetUserByEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user!;
        }

        public List<AppUser> GetAllUsers()
        {
            var users = userManager.Users.ToList();
            return users;
        }
    }
}
