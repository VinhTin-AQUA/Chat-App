using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace ChatAppServer.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthRepository(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

        public async Task<SignInResult> LoginAsync(AppUser user, string password)
        {
            var r = await signInManager.CheckPasswordSignInAsync(user, password, true);
            return r;
        }
    }
}
