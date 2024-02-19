using ChatAppServer.Repositories;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Types.IType;
using ChatAppServer.Services;
using ChatAppServer.Services.IServices;
using System.Security.Claims;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class AuthQuery
    {
        public async Task<ResultType> GetRefreshRoken(string email, [Service]IAuthRepository userRepository, [Service] IJwtService jwtService)
        {
            var user = await userRepository.GetUserByEmail(email);
            if(user == null)
            {
                return new ResultType(true, ["User not found"], null);
            }

            //var t = new IResultType();

            return new ResultType(true, [], new UserType
            {
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
                Token = await jwtService.CreateJwt(user),
                UniqueCodeUser = user.UniqueCodeUser,
            });
        }
    }
}
