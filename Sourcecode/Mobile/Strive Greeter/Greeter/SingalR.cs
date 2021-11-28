using System;
using System.Threading.Tasks;
using Greeter.Common;
using Microsoft.AspNetCore.SignalR.Client;

namespace Greeter
{
    public class SingalR
    {
        public static HubConnection hubConnection;

        async Task StartConnection()
        {
            if (hubConnection == null)
            {
                var hubConnection = new HubConnectionBuilder()
                .WithUrl(Urls.BASE_URL + "/chatMessageHub")
                .Build();

                await hubConnection.StartAsync();
            }

            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection?.StartAsync();
                //await SendEmployeeCommunicationId(EmployeeTempData.EmployeeID.ToString(), ConnectionID);
                //await SubscribeChatEvent();
                //var communicationData = new ChatCommunication()
                //{
                //    communicationId = ConnectionID,
                //    employeeId = EmployeeTempData.EmployeeID
                //};

                //await MessengerService.ChatCommunication(communicationData);
            }
        }

        async Task SendMessage(string user, string message)
        {
            var hubConnection = new HubConnectionBuilder()
               .WithUrl("{https://yoururlhere.com or ip:port or localhost:port" + "/chatHub")
               .Build();

            await hubConnection.InvokeAsync("SendMessage", user, message);
        }

        async Task ReceiveMsg()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl("{https://yoururlhere.com or ip:port or localhost:port" + "/chatHub")
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                //do something on your UI maybe?
            });
        }
    }
}
