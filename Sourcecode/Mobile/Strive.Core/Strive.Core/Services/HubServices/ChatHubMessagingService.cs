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

namespace Strive.Core.Services.HubServices
{
    public static class ChatHubMessagingService
    {
        
        public static string ConnectionID { get; set; }
        public static string RecipientsConnectionID { get; set; }
        public static HubConnection connection;
        public static List<SendChatMessage> receiverPrivateMessages { get; set; }
        public static ObservableCollection<SendChatMessage> PrivateMessageList { get; set; }

        //meh! Just to connect to the so called "SERVER"
        public static async Task<string> StartConnection()
        {
            if (string.IsNullOrEmpty(ConnectionID) || connection == null)
            {
                //connection = new HubConnectionBuilder().WithUrl("http://10.0.2.2:60001/ChatMessageHub").Build();
                connection = new HubConnectionBuilder().WithUrl(ApiUtils.BASE_URL + "/ChatMessageHub").Build();
                try
                {
                    await connection?.StartAsync();          
                    PrivateMessageList = new System.Collections.ObjectModel.ObservableCollection<SendChatMessage>();
                }
                catch (Exception ex)
                {
                    ConnectionID = null;
                    Console.WriteLine(ex.Message);
                }
            }
            if(connection.State == HubConnectionState.Disconnected)
            {
                await connection?.StartAsync();
            }
            connection.Closed += Connection_Closed;
            ConnectionID = connection.ConnectionId;
            return ConnectionID;
        }

        private static async Task Connection_Closed(Exception arg)
        {
            ConnectionID = null;
            await StartConnection();
            ConnectionID = connection.ConnectionId;
        }


        public static async Task SubscribeChatEvent()
        {
            connection.On<string>("OnDisconnected", (data) => {

                Console.WriteLine("Connection is disconnected !");

            });

            connection.On<string>("ReceiveCommunicationID", (id) => {
               
            });

            connection?.On<object>("ReceivePrivateMessage", (data) => {
                Console.WriteLine("Private Message received", data);
                try
                {
                    var datas = JsonConvert.DeserializeObject<SendChatMessage>(data.ToString());
                    PrivateMessageList.Add(datas);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            connection?.On<object>("ReceiveGroupMessage", (data) => {

                Console.WriteLine("Group Message received", data);

            });

            connection?.On<string>("SendPrivateMessage", (data) => {

                Console.WriteLine("Private Message sent", data);

            });

            connection?.On<string>("ReceiveEmployeeCommunicationId", (data) => {

                Console.WriteLine("Employee Communication ID", data);

            });

            connection?.On<string>("UserAddedtoGroup", (data) => {

                Console.WriteLine("User added", data);

            });

            connection?.On<object>("UserLogOutNotification", (data) => {

                StopConnection();

            });

            connection?.On<object>("GroupMessageReceive", (data) => {

                Console.WriteLine("Group Message Received", data);

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
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public static async void SendEmployeeCommunicationId(string empID, string commID)
        {
            try
            {
                await connection.InvokeAsync("SendEmployeeCommunicationId", empID, commID);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }

        public static async void SendMessageToGroup(SendChatMessage groupInfo)
        {
            try
            {
                await connection.InvokeAsync("SendMessageToGroup", groupInfo.groupId,EmployeeTempData.EmployeeID,groupInfo.groupName,groupInfo.chatMessage.messagebody );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
