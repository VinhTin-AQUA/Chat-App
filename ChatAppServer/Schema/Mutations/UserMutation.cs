using AppAny.HotChocolate.FluentValidation;
using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Types.InputTypes;
using ChatAppServer.Schema.Validators;

namespace ChatAppServer.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
        public async Task<UserType> CreateUser([UseFluentValidation, UseValidator<RegisterInputTypeValidator>()]RegisterInputType model, [Service]IUserRepository userRepository)
        {
            var userExist = await userRepository.GetUserByEmail(model.Email);
            if(userExist != null)
            {
                throw new GraphQLException(new List<Error>
                {
                    new Error("Email already exists")
                });
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
                var err = r.Errors.Select(e => new Error(e.Description)).ToList();
                throw new GraphQLException(err);
            }

            return new UserType
            {
                Email = user.Email,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
                PhoneNumber = user.PhoneNumber!,
                FriendIds = user.FriendIds,
                GroupIds = user.GroupIds
            };
        }
    }
}
