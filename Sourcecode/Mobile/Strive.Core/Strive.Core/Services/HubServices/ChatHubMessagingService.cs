using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Strive.Core.Utils.Employee;
using Strive.Core.Utils;
using MvvmCross.Base;
using Strive.Core.Models.Employee.Messenger.PersonalChat;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.Interfaces;
using MvvmCross;

namespace Strive.Core.Services.HubServices
{
    public static class ChatHubMessagingService
    {

        public static string ConnectionID { get; set; }
        public static string EmpID { get; set; }
        public static string EmpConnectionID { get; set; }
        public static string RecipientsConnectionID { get; set; }
        public static HubConnection connection;
        public static List<SendChatMessage> receiverPrivateMessages { get; set; }
        public static ObservableCollection<SendChatMessage> PrivateMessageList { get; set; }
        public static ObservableCollection<SendChatMessage> GroupMessageList { get; set; }
        public static ObservableCollection<RecipientsCommunicationID> RecipientsID { get; set; }
        public static IMessengerService MessengerService = Mvx.IoCProvider.Resolve<IMessengerService>();
        

        //meh! Just to connect to the so called "SERVER"
        public static async Task<string> StartConnection()
        {
            if (string.IsNullOrEmpty(ConnectionID) || connection == null)
            {
                //connection = new HubConnectionBuilder().WithUrl("http://10.0.2.2:60001/ChatMessageHub").Build();
                connection = new HubConnectionBuilder().WithUrl(ApiUtils.AZURE_URL_TEST + "/chatMessageHub").Build();
                try
                {
                    await connection?.StartAsync();
                    Console.WriteLine("Connection established Successfully! ");
                    PrivateMessageList = new ObservableCollection<SendChatMessage>();
                    GroupMessageList = new ObservableCollection<SendChatMessage>();
                }
                catch (Exception ex)
                {
                    ConnectionID = null;
                    Console.WriteLine(ex.Message);
                }
            }
            if (connection.State == HubConnectionState.Disconnected)
            {
                await connection?.StartAsync();
                await SendEmployeeCommunicationId(EmployeeTempData.EmployeeID.ToString(), ConnectionID);
                await SubscribeChatEvent();
                var communicationData = new ChatCommunication()
                {
                    communicationId = ConnectionID,
                    employeeId = EmployeeTempData.EmployeeID
                };
                await MessengerService.ChatCommunication(communicationData);
            }
            connection.Closed += Connection_Closed;
            ConnectionID = connection.ConnectionId;

            var chatcommunication = new ChatCommunication();
            chatcommunication.communicationId = ConnectionID;
            chatcommunication.employeeId = EmployeeTempData.EmployeeID;
            var tempresult = await MessengerService.ChatCommunication(chatcommunication);
            return ConnectionID;
        }

        private static async Task Connection_Closed(Exception arg)
        {
            ConnectionID = null;
            await StartConnection();
            ConnectionID = connection.ConnectionId;
            await SubscribeChatEvent();
            await SendEmployeeCommunicationId(EmployeeTempData.EmployeeID.ToString(), ConnectionID);
            var communicationData = new ChatCommunication()
            {
                communicationId = ConnectionID,
                employeeId = EmployeeTempData.EmployeeID
            };
            await MessengerService.ChatCommunication(communicationData);
        }


        public static async Task SubscribeChatEvent()
        {
            connection.On<string>("OnDisconnected", (data) =>
            {

                Console.WriteLine("Connection is disconnected !");

            });

            connection.On<object>("ReceiveCommunicationID", (id) =>
            {

            });

            connection?.On<object>("ReceivePrivateMessage", (data) =>
            {
                Console.WriteLine("Private Message received", data);
                try
                {
                    var datas = JsonConvert.DeserializeObject<SendChatMessage>(data.ToString());
                    PrivateMessageList.Add(datas);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

            connection?.On<object>("ReceiveGroupMessage", (data) =>
            {
                Console.WriteLine("Group Message received", data);
                try
                {
                    var datas = JsonConvert.DeserializeObject<SendChatMessage>(data.ToString());
                    GroupMessageList.Add(datas);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            connection?.On<string>("SendPrivateMessage", (data) =>
            {
                Console.WriteLine("Private Message sent", data);

            });

            connection?.On<string>("SendGroupMessage", (data) =>
            {

                Console.WriteLine("Group Message sent", data);

            });

            connection?.On<object>("ReceiveEmployeeCommunicationId", (data) =>
            {
                Console.WriteLine("new communication id received", data);
                try
                {
                    var datas = JsonConvert.DeserializeObject<string[]>(data.ToString());
                    RecipientsID.Add(new RecipientsCommunicationID() { employeeId = datas[0], communicationId = datas[1] });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            connection?.On<string>("UserAddedtoGroup", (data) =>
            {

                Console.WriteLine("User added", data);

            });

            connection?.On<object>("UserLogOutNotification", (data) =>
            {

                StopConnection();

            });

            connection?.On<object>("GroupMessageReceive", (data) =>
            {

                Console.WriteLine("Group Message Received", data);
                try
                {
                    var datas = JsonConvert.DeserializeObject<SendChatMessage>(data.ToString());
                    GroupMessageList.Add(datas);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

        }

        public static async void StopConnection()
        {
            try
            {
                await connection.InvokeAsync("SendEmployeeCommunicationId", EmployeeTempData.EmployeeID, "0");
                await connection.StopAsync();
                ConnectionID = null;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static async Task SendEmployeeCommunicationId(string empID, string commID)
        {
            try
            {
                await connection.InvokeAsync("SendEmployeeCommunicationId", empID, commID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public static async void SendMessageToGroup(SendChatMessage groupInfo)
        {
            try
            {
                await connection.InvokeAsync("SendMessageToGroup", groupInfo.groupId, EmployeeTempData.EmployeeID, groupInfo.groupName, groupInfo.chatMessage.messagebody);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
      
    }
}
