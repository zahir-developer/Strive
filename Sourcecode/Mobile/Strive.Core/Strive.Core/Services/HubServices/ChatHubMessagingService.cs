using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Strive.Core.Utils.Employee;

namespace Strive.Core.Services.HubServices
{
    public static class ChatHubMessagingService
    {

        public static string ConnectionID { get; set; }
        public static string RecipientsConnectionID { get; set; }
        public static HubConnection connection;


        //meh! Just to connect to the so called "SERVER"
        public static async Task<string> StartConnection()
        {
            if (string.IsNullOrEmpty(ConnectionID))
            {
                connection = new HubConnectionBuilder().WithUrl("http://14.141.185.75:5004/ChatMessageHub").Build();
                try
                {
                    await connection?.StartAsync();
                    ConnectionID = connection.ConnectionId;
                }
                catch (Exception ex)
                {
                    ConnectionID = null;
                    Console.WriteLine(ex.Message);
                }
            }
            return ConnectionID;
        }


        public static async Task SubscribeChatEvents()
        {
            try
            {
                connection?.On<string>("OnDisconnected", (data) => {

                    Console.WriteLine("Connection has been disconnected !");

                });

                connection?.On<string>("ReceiveCommunicationID", (id) => {
                    ConnectionID = id;
                    SendEmployeeCommunicationId();
                });

                connection?.On<string>("ReceivePrivateMessage", (data) => {

                    Console.WriteLine("Private Message received", data);

                });

                connection?.On<string>("ReceiveGroupMessage", (data) => {

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

                connection?.On<string>("GroupMessageReceive", (data) => {

                    Console.WriteLine("Group Message Received", data);

                });
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async void SendEmployeeCommunicationId()
        {
            try
            {
               await connection?.InvokeAsync("SendEmployeeCommunicationId", EmployeeTempData.EmployeeID, ConnectionID);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
