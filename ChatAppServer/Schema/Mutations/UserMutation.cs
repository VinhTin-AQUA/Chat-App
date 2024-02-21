using AppAny.HotChocolate.FluentValidation;
using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Types.DTOTypes;
using ChatAppServer.Schema.Validators;

namespace ChatAppServer.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
        public async Task<ResultType> CreateUser([UseFluentValidation, UseValidator<RegisterInputTypeValidator>()] RegisterInputType model, [Service] IAuthRepository userRepository)
        {
            var userExist = await userRepository.GetUserByEmail(model.Email);
            if (userExist != null)
            {
                return new ResultType(false, ["Email already exists"], null);
            }

            AppUser user = new AppUser
            {
                Email = model.Email,
                FullName = model.FullName,
                UserName = model.Email,
                AvatarUrl = model.AvatarUrl,
                PhoneNumber = "",
                UniqueCodeUser = Guid.NewGuid().ToString()[..8]
            };

            var r = await userRepository.CreateUserAsync(user, model.Password);
            if (r.Succeeded == false)
            {
                var err = r.Errors.Select(e => e.Description).ToList();
                return new ResultType(false, err, null);
            }

            return new ResultType(true, [], new UserType
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
                UniqueCodeUser = user.UniqueCodeUser,
                Token = "",
            });
        }

        public async Task<ResultType> AddUserToGroup(string uniqueCodeUser, string uniqueCodeGroup, [Service] IUserRepository userRepository, [Service]IAuthRepository authRepository)
        {
            await userRepository.AddUserToGroup(uniqueCodeUser, uniqueCodeGroup);
            var user = authRepository.GetUserByUniqueCode(uniqueCodeUser);

            if(user == null)
            {
                return new ResultType(false, ["User not found"], null);
            }

            return new ResultType(true, [], new UserType
            {
                AvatarUrl = user.AvatarUrl,
                Email = user.Email!,
                FullName = user.FullName,
                PhoneNumber = user.PhoneNumber,
                Token = "",
                UniqueCodeUser = user.UniqueCodeUser,
            });
        }
    }
}
