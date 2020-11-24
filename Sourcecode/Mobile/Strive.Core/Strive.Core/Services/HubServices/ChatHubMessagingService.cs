using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Strive.Core.Utils.Employee;
using Strive.Core.Utils;

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
                // connection = new HubConnectionBuilder().WithUrl("http://10.0.2.2:60001/ChatMessageHub").Build();
                connection = new HubConnectionBuilder().WithUrl(ApiUtils.BASE_URL + "/ChatMessageHub").Build();
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
        public static async Task SubscribeChatEvent()
        {
            connection.On<string>("OnDisconnected", (data) => {

                Console.WriteLine("Connection is disconnected !");

            });

            connection.On<string>("ReceiveCommunicationID", (id) => {

                connection.InvokeAsync("SendEmployeeCommunicationId", EmployeeTempData.EmployeeID, id);
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

        public static async Task StopConnection()
        {
            try
            {
                await connection.InvokeAsync("SendEmployeeCommunicationId", EmployeeTempData.EmployeeID, '0');
                await connection.StopAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
