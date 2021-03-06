using Microsoft.AspNetCore.SignalR;
using Strive.BusinessLogic.Messenger;
using System;
using System.Threading.Tasks;

namespace Admin.API
{
    public class ChatMessageHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Clients.Client(Context.ConnectionId).SendAsync("ReceiveCommunicationID", Context.ConnectionId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Clients(Context.ConnectionId).SendAsync("UserLogOutNotification", Context.ConnectionId);

            return base.OnDisconnectedAsync(exception);
        }

        //public async Task SendPrivateMessage(string[] obj)// string connectionId, string employeeId, string userName, string message)
        //{
        //    if (Clients != null)
        //    {
        //        //string[] obj = new string[] { connectionId, employeeId, firstName, lastName, initial, message};
        //        await Clients.Client(obj[0]).SendAsync("ReceivePrivateMessage", obj);
        //    }
        //}

        public async Task JoinGroup(string connectionId, string userName, string groupName)
        {
            await Clients.Group(groupName).SendAsync("GroupMessageReceive", userName + " has joined.");
            await Groups.AddToGroupAsync(connectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string employeeId, string userName, string message)
        {
            if (Clients != null)
            {
                string[] obj = new string[] { groupName, employeeId, userName, message };
                await Clients.Group(groupName).SendAsync("ReceiveGroupMessage", obj);
            }
        }

        public async Task SendEmployeeCommunicationId(string employeeId, string commId)
        {
            if (Clients != null)
            {
                string[] obj = new string[] { employeeId, commId };
                await Clients.All.SendAsync("ReceiveEmployeeCommunicationId", obj);
            }
        }

    }
}