using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Types.DTOTypes;
using ChatAppServer.Services;
using ChatAppServer.Services.IServices;
using HotChocolate.Authorization;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        public async Task<UserType?> GetUserByEmail(string email, [Service] IAuthRepository authRepository)
        {
            var user = await authRepository.GetUserByEmail(email);
            if (user == null)
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

        public List<UserType> GetAllUsers([Service] IAuthRepository authRepository)
        {
            var users = authRepository.GetAllUsers();
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

        public async Task<ResultType> Login(LoginInputType model, [Service] IAuthRepository authRepository, [Service] IJwtService jwtService)
        {
            if (model == null)
            {
                return new ResultType(false, [""], null);
            }

            var user = await authRepository.GetUserByEmail(model.Email);
            if (user == null)
            {
                return new ResultType(false, ["Email or password is incorrect"], null);
            }

            var loginResult = await authRepository.LoginAsync(user, model.Password);
            if (loginResult.Succeeded == false)
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

        public async Task<List<UserType>> SearchUserByName(string name, [Service] IUserRepository userRepository)
        {
            if(string.IsNullOrEmpty(name))
            {
                return new List<UserType>();
            }
            var users = await userRepository.SearchUsersByName(name);

            var userTypes = users.Select(user => new UserType
            {
                Email = user.Email!,
                PhoneNumber = user.PhoneNumber,
                FriendIds = user.FriendIds,
                GroupIds = user.GroupIds,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
            }).ToList();

            return userTypes;
        }
    }
}
