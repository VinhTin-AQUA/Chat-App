using ChatAppServer.Models;
using MongoDB.Driver.Core.Connections;

namespace ChatAppServer.Services
{
    public class ChatService
    {
        public static Dictionary<string, LinkedList<UserOnline>> OnlineUsers { get; set; } = new Dictionary<string, LinkedList<UserOnline>>();

        public bool AddUserOnline(string groupName, string userName, string connectionId)
        {
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(groupName) == false)
                {
                    OnlineUsers.Add(groupName, new LinkedList<UserOnline>());

                    var userOndline = new UserOnline
                    {
                        FullName = userName,
                        ConnectionId = connectionId
                    };

                    OnlineUsers[groupName].AddLast(userOndline);
                    return true;
                }

                var user = OnlineUsers[groupName];
                //if (user.Contains(userName))
                //{
                //    return true;
                //}

                var userOnline = new UserOnline
                {
                    FullName = userName,
                    ConnectionId = connectionId
                };
                OnlineUsers[groupName].AddLast(userOnline);
                return true;
            }
        }

        public bool RemoveUserOnline(string connectionId)
        {
            lock (OnlineUsers)
            {
                foreach(var userGroups in OnlineUsers)
                {
                    foreach(var user in userGroups.Value)
                    {
                        if(user.ConnectionId == connectionId)
                        {
                            userGroups.Value.Remove(user);
                            break;
                        }
                    }
                }
                return false;
            }
        }

        public List<string> GetUsersOnlineAGroup(string groupName)
        {
            lock (OnlineUsers)
            {
                if (OnlineUsers.ContainsKey(groupName))
                {
                    return OnlineUsers[groupName].Select(u => u.FullName).ToList();
                }
                return new List<string>();
            }
        }
    }
}
