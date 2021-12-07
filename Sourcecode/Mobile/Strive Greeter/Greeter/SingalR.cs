using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.Modules.Message;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace Greeter
{
    public static class SingalR
    {
        public static HubConnection hubConnection;

        public static async Task StartConnection(string empId = null, string connectionId = null)
        {
            if (hubConnection == null)
            {
                await ConnectHubAndSubscribeForEvents();
            }
            else if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection?.StartAsync();
                Debug.WriteLine("hubConnection.ConnectionId : " + hubConnection.ConnectionId);
                await SendEmployeeCommunicationId(empId.ToString(), hubConnection.ConnectionId);
                //await SubscribeChatEvent();
                //var communicationData = new ChatCommunication()
                //{
                //    communicationId = ConnectionID,
                //    employeeId = EmployeeTempData.EmployeeID
                //};

                //await MessengerService.ChatCommunication(communicationData);
            }

            hubConnection.Closed += async (arg) => await ConnectHubAndSubscribeForEvents();
        }

        public static async Task SendEmployeeCommunicationId(string empID, string commID)
        {
            try
            {
                await hubConnection.InvokeAsync("SendEmployeeCommunicationId", empID, commID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        static async Task<HubConnection> ConnectToHub()
        {
            var hubConnection = new HubConnectionBuilder()
                .WithUrl(Urls.BASE_URL + "/chatMessageHub")
                .Build();

            await hubConnection.StartAsync();
            return hubConnection;
        }

        static async Task ConnectHubAndSubscribeForEvents()
        {
            hubConnection = await ConnectToHub();
            await SubscribeChatEvent();
        }

        //async Task SendMessage(string user, string message)
        //{
        //    var hubConnection = new HubConnectionBuilder()
        //       .WithUrl(Urls.BASE_URL + "/chatHub")
        //       .Build();

        //    await hubConnection.InvokeAsync("SendMessage", user, message);
        //}

        //async Task ReceiveMsg()
        //{
        //    var hubConnection = new HubConnectionBuilder()
        //        .WithUrl(Urls.BASE_URL + "/chatHub")
        //        .Build();

        //    hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
        //    {
        //        //do something on your UI maybe?
        //    });
        //}

        public static async Task SubscribeChatEvent()
        {
            hubConnection?.On<object>("ReceivePrivateMessage", (data) =>
            {
                Console.WriteLine("Private Message received", data);
                try
                {
                    //PrivateMessageList.Add(datas);
                    //var dict = new NSDictionary(new NSString("chatMsg"), new NSString(data.ToString()));
                    var dict = new NSDictionary(new NSString("chatMsg"), new NSString(JsonConvert.SerializeObject(data.ToString())));
                    NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.greeter.private_message_received"), null, dict);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            hubConnection?.On<object>("UserLogOutNotification", (data) =>
            {
                StopConnection(123);
            });
        }

        public static async void StopConnection(long empId)
        {
            try
            {
                await hubConnection.InvokeAsync("SendEmployeeCommunicationId", empId, "0");
                await hubConnection.StopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async void SendMessageToGroup(long groupId, long empId, string groupName, string msg)
        {
            try
            {
                await hubConnection.InvokeAsync("SendMessageToGroup", groupId, empId, groupName, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
