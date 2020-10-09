using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace WebAPI
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            //Console.WriteLine("--> Connection Opened: " + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.Group("Grp1").SendAsync("GroupMessageReceive", " has joined.");
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task JoinRoom(string connectionId, string userName, string groupName)
        {
            await Clients.Group(groupName).SendAsync("GroupMessageReceive", userName + " has joined.");
            await Groups.AddToGroupAsync(connectionId, groupName);
        }

        public async Task JoinGroup(string connectionId, string userName, string groupName)
        {
            await Clients.Group(groupName).SendAsync("GroupMessageReceive", userName + " has joined.");
            await Groups.AddToGroupAsync(connectionId, groupName);
        }

        public async Task LeaveRoom(string connectionId, string userName, string groupName)
        {
            await Clients.Group(groupName).SendAsync("GroupMessageReceive", userName + " has left.");
            await Groups.RemoveFromGroupAsync(connectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string userName, string message)
        {
            string[] obj = new string[] { userName, message };
            await Clients.Group(groupName).SendAsync("GroupMessageReceive", obj);
        }

        public async Task SendPrivateMessage(string connectionId, string userName, string message)
        {
            string[] obj = new string[] { userName, message };
            await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", obj);
        }
    }
}