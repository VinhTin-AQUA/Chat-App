using ChatAppServer.Models;
using ChatAppServer.Repositories.IRepositories;

namespace ChatAppServer.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        public string GetUserName()
        {
            return "ho vinh tin";
        }
    }
}
