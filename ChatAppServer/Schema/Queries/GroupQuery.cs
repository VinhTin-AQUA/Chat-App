using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Schema.Types;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class GroupQuery
    {
        public async Task<List<GroupType>?> GetGroupsOfUser(string ownerId, [Service] IGroupRepository groupRepository)
        {
           var groups = await groupRepository.GetGroupsOfUser(ownerId);

            return groups.Select(g => new GroupType
            {
                GroupName = g.GroupName,
                Id = g.Id,
                OwnerId = ownerId,
                Password = g.Password,
                UniqueCodeGroup = g.UniqueCodeGroup 
            }).ToList();
        }
    
    
        public async Task<List<string>> GetGroup(string uniqueCodeGroup, [Service] IGroupRepository groupRepository, [Service]IAuthRepository authRepository)
        {
            var group = await groupRepository.GetGroup(uniqueCodeGroup);

            List<string> memberNames = [];
            foreach (var member in group.Members)
            {
                var user = authRepository.GetUserByUniqueCode(member);
                memberNames.Add(user.FullName);
            }

            return memberNames;
        }
    }
}
