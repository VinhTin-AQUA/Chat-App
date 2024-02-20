using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;
using ChatAppServer.Schema.Types.DTOTypes;

namespace ChatAppServer.Schema.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class GroupMutation
    {
        public async Task<GroupType> CreateGroupToUser(GroupTypeInput model, string email, [Service]IGroupRepository groupRepository, [Service]IAuthRepository authRepository, [Service]IUserRepository userRepository)
        {
            var user = await authRepository.GetUserByEmail(email);
            if (user == null)
            {
                return null!;
            }

            var group = new Group
            {
                OwnerId = user.UniqueCodeUser,
                GroupName = model.GroupName,
                Password = model.Password,
                UniqueCodeGroup = Guid.NewGuid().ToString()[..8],
            };

            await groupRepository.CreateGroupToUser(group);
            await userRepository.AddGroupUniqueCodeToUser(email, group.UniqueCodeGroup);

            return new GroupType
            {
                Id = group.Id,
                OwnerId = group.OwnerId,
                GroupName = group.GroupName,
                Password = group.Password,
                UniqueCodeGroup = group.UniqueCodeGroup,
            };
        }

  
    }
}
