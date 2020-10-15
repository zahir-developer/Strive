using Microsoft.AspNetCore.SignalR;
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

        public async Task SendPrivateMessage(string connectionId, string employeeId, string userName, string message)
        {
            if (Clients != null)
            {
                string[] obj = new string[] { connectionId, employeeId, userName, message };
                await Clients.Client(connectionId).SendAsync("ReceivePrivateMessage", obj);
            }
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