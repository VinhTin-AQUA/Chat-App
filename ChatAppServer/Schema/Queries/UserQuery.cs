using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;

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
    }
}
