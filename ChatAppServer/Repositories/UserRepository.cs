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

        public async Task CreateUserAsync(AppUser user, string password)
        {
            var r = await userManager.CreateAsync(user, password);
        }
    }
}
