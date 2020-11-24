using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace Strive.Core.Services.HubServices
{
    public static class ChatHubMessagingService
    {

        public static string ConnectionID { get; set; }
        public static HubConnection connection;


        //meh! Just to connect to the so called "SERVER"
        public static async Task<string> StartConnection()
        {
            if (string.IsNullOrEmpty(ConnectionID))
            {
                connection = new HubConnectionBuilder().WithUrl("http://14.141.185.75:5004/ChatMessageHub").Build();
                try
                {
                    await connection.StartAsync();
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



    }
}
