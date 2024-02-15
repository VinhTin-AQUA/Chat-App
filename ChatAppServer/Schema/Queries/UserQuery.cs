using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Types.DTOTypes;
using ChatAppServer.Services.IServices;
using HotChocolate.Authorization;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        public async Task<UserType?> GetUserByEmail(string email, [Service] IUserRepository userRepository)
        {
            var user = await userRepository.GetUserByEmail(email);
            if(user == null)
            {
                return null!;
            }
            return new UserType
            {
                Email = user.Email!,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                FriendIds = user.FriendIds,
                GroupIds = user.GroupIds,
            };
        }

        [Authorize]
        public List<UserType> GetAllUsers([Service] IUserRepository userRepository)
        {
            var users = userRepository.GetAllUsers();
            return users.Select(user => new UserType
            {
                Email = user.Email!,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                AvatarUrl = user.AvatarUrl,
                FriendIds = user.FriendIds,
                GroupIds = user.GroupIds,
            }).ToList();
        }

        public async Task<ResultType> Login(LoginInputType model, [Service] IUserRepository userRepository, [Service]IJwtService jwtService)
        {
            if(model == null)
            {
                return new ResultType(false, [""], null);
            }

            var user = await userRepository.GetUserByEmail(model.Email);
            if(user == null)
            {
                return new ResultType(false, ["Email or password is incorrect"], null);
            }

            var loginResult = await userRepository.LoginAsync(user, model.Password);
            if(loginResult.Succeeded == false)
            {
                return new ResultType(false, ["Email or password is incorrect"], null);
            }

            return new ResultType(true, [""], new UserType
            {
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                FriendIds = user.FriendIds,
                GroupIds = user.GroupIds,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
                Token = await jwtService.CreateJwt(user)
            });
        }
    }
}
