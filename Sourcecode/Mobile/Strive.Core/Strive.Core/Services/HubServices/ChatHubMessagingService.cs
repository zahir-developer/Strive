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

        //to maintain the connection ID ...bruh this is really an expensive process !
        public static async Task SubscribeChatEvents()
        {
            connection?.On<dynamic>("OnDisconnected", (data) => {

                Console.WriteLine("Connection has been disconnected !", data);
            });

            connection?.On<dynamic>("ReceiveCommunicationID", (id) => {

                RecipientsConnectionID = id;
                Console.WriteLine("Communication ID", id);
                try
                {
                    connection.InvokeAsync("SendEmployeeCommunicationId");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

        
        
        }



    }
}
