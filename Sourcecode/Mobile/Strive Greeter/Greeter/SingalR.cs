using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Modules.Message;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace Greeter
{
    public static class SingalR
    {
        public static HubConnection hubConnection;

        public static async Task StartConnection(long empId = -1)
        {
            if (hubConnection == null)
            {
                await ConnectHubAndSubscribeForEvents();
                await SendEmployeeCommunicationId(empId.ToString(), hubConnection.ConnectionId);
                _ = UpdateApi(empId);
            }
            else if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection?.StartAsync();
                await SubscribeChatEvent();
                Debug.WriteLine("hubConnection.ConnectionId : " + hubConnection.ConnectionId);
                await SendEmployeeCommunicationId(empId.ToString(), hubConnection.ConnectionId);
                _ = UpdateApi(empId);
            }

            hubConnection.Closed += async (arg) => {
                hubConnection = null;
                await StartConnection(empId);
            };
        }

        static async Task UpdateApi(long empId)
        {
            var communicationData = new ChatCommunication()
            {
                CommunicationID = hubConnection.ConnectionId,
                EmpID = empId
            };

            var response = await SingleTon.MessageApiService.ChatCommunication(communicationData);
        }

        static async Task SendEmployeeCommunicationId(string empID, string commID)
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
                    //var json = JsonConvert.SerializeObject();

                    var dict = new NSDictionary(new NSString("chatMsg"), new NSString(data.ToString()));
                    NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.greeter.message_received"), null, dict);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            hubConnection?.On<object>("ReceiveGroupMessage", (data) =>
            {
                Console.WriteLine("Group Message received", data);
                try
                {
                    //var datas = JsonConvert.DeserializeObject<SendChatMessage>(data.ToString());
                    var dict = new NSDictionary(new NSString("groupChatMsg"), new NSString(data.ToString()));
                    NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.greeter.message_received"), null, dict);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            hubConnection?.On<object>("UserLogOutNotification", (data) =>
            {
                StopConnection(AppSettings.UserID);
            });

            hubConnection?.On<object>("ReceiveEmployeeCommunicationId", (data) =>
            {
                Console.WriteLine("new communication id received", data);
                try
                {
                    var datas = JsonConvert.DeserializeObject<string[]>(data.ToString());
                    //RecipientsID.Add(new RecipientsCommunicationID() { employeeId = datas[0], communicationId = datas[1] });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });
        }

        public static async void StopConnection(long empId)
        {
            try
            {
                await hubConnection.InvokeAsync("SendEmployeeCommunicationId", empId, "0");
                await hubConnection.StopAsync();
                hubConnection = null;
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
