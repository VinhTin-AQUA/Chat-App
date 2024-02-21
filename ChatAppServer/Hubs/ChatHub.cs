
using ChatAppServer.Models;
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

        // disconnect khi refresh trang
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var onlineUser = chatService.RemoveUserOnline(Context.ConnectionId);
            if(onlineUser != null)
            {
                await Clients.Group(onlineUser.GroupName).SendAsync("UserLeft", onlineUser.FullName);
            }
            await base.OnDisconnectedAsync(exception);
        }

        // người dùng vào 1 group và kết nối với group
        public async Task ConnectToGroup(string uniqueCodeGroup, string userName)
        {
            chatService.AddUserOnline(uniqueCodeGroup, userName, Context.ConnectionId);

            await Groups.AddToGroupAsync(Context.ConnectionId, uniqueCodeGroup);

            // khi có người dùng khác join group
            await Clients.Group(uniqueCodeGroup).SendAsync("OtherUserConnected", userName);

            // khi người sử dụng join group
            await Clients.Client(Context.ConnectionId).SendAsync("UserConnected", ChatService.OnlineUsers[uniqueCodeGroup].Select(p => p.FullName).ToList());
        }


        // ngắt kết nối khi chuyển url
        public async Task DisConnectGroup(string uniqueCodeGroup)
        {
            var onlineUser = chatService.RemoveUserOnline(Context.ConnectionId);
            if (onlineUser != null)
            {
                await Clients.Group(onlineUser.GroupName).SendAsync("UserLeft", onlineUser.FullName);
            }
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, uniqueCodeGroup);
        }

        // mời người khác vào group
        //public async Task AddUserToGroup(string uniqueCodeUser, string uniqueCodeGroup)
        //{
        //    var user = authRepository.GetUserByUniqueCode(uniqueCodeUser);
        //    chatService.AddUserOnline(uniqueCodeGroup, user.FullName, Context.ConnectionId);
        //    await Clients.Caller.SendAsync("AddedUserToGroup", user.FullName);
        //}

        // gửi tin nhắn đến group => chuyển về client để người khác nhận tin nhắn
        public async Task SendMessageToGroup(string uniqueCodeGroup, Message message)
        {
            await Clients.OthersInGroup(uniqueCodeGroup).SendAsync("OtherMessageSent", message);
        }

        // rời group

        // được người khác mời vào group



    }
}
