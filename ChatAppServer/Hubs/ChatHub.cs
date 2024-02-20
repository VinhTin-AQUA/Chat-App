
using ChatAppServer.Repositories.IRepositories;
using ChatAppServer.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppServer.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatService chatService;
        private readonly IAuthRepository authRepository;

        public ChatHub(ChatService chatService, IAuthRepository authRepository)
        {
            this.chatService = chatService;
            this.authRepository = authRepository;
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            chatService.RemoveUserOnline(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        // người vào 1 group và kết nối với group
        public async Task ConnectToGroup(string groupName, string userName)
        {
            chatService.AddUserOnline(groupName, userName, Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("UserConnected", userName);
        }


        // ngắt kết nối khi chuyển url
        public async Task DisConnectGroup(string groupName)
        {
            chatService.RemoveUserOnline(Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        // mời người khác vào group
        public async Task AddUserToGroup(string uniqueCodeUser, string groupName)
        {
            var user = authRepository.GetUserByUniqueCode(uniqueCodeUser);
            chatService.AddUserOnline(groupName, user.FullName, Context.ConnectionId);
            await Clients.Caller.SendAsync("AddedUserToGroup", user.FullName);
        }



        // gửi tin nhắn đến group

        // rời group

        // được người khác mời vào group

        // người khác nhắn tới group
    }
}
