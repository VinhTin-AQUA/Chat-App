using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;

namespace ChatAppServer.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutation
    {
        //private readonly IUserRepository userRepository;

        //public UserMutation(IUserRepository userRepository)
        //{
        //    this.userRepository = userRepository;
        //}


        public async Task<UserType> CreateUser([Service]IUserRepository userRepository)
        {
            AppUser user = new AppUser
            {
                Email = "abcdef123@gmail.com",
                FullName = "Hồ Vĩnh AAAAAAAAA",
                UserName = "abcdef123@gmail.com",
            };
            await userRepository.CreateUserAsync(user, "abcdef123");

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
