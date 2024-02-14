using ChatAppServer.Models;

namespace ChatAppServer.Services.IServices
{
    public interface IJwtService
    {
        Task<string> CreateJwt(AppUser user);
    }
}
