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
        public async Task<ResultType> CreateUser([UseFluentValidation, UseValidator<RegisterInputTypeValidator>()]RegisterInputType model, [Service]IUserRepository userRepository)
        {
            var userExist = await userRepository.GetUserByEmail(model.Email);
            if(userExist != null)
            {
                return new ResultType(false, ["Email already exists"], null);
            }

            AppUser user = new AppUser
            {
                Email = model.Email,
                FullName = model.FullName,
                UserName = model.Email,
                AvatarUrl = model.AvatarUrl,
                PhoneNumber = ""
            };

            var r = await userRepository.CreateUserAsync(user, model.Password);
            if(r.Succeeded == false)
            {
                var err = r.Errors.Select(e => e.Description).ToList();
                return new ResultType(false, err, null);
            }

            return new ResultType(true, [], new UserType
            {
                Email = user.Email,
                PhoneNumber= user.PhoneNumber,
                FriendIds = user.FriendIds,
                GroupIds = user.GroupIds,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
            });
        }

        
    }
}
