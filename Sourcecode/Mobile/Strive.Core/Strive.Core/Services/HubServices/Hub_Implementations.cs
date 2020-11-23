using Microsoft.AspNetCore.SignalR.Client;
using Strive.Core.Utils.Employee;
using System;
using System.Threading.Tasks;

namespace Strive.Core.Services.HubServices
{
    public static class Hub_Implementations 
    {
        public static string ConnectionID { get; set; }
        public static HubConnection connection;


        //meh! Just to connect to the so called "SERVER"
        public static async Task<string> StartConnection()
        {
            if(string.IsNullOrEmpty(ConnectionID))
            {
                connection = new HubConnectionBuilder().WithUrl("http://14.141.185.75:5004/ChatMessageHub").Build();
                try
                {
                    await connection.StartAsync();
                    ConnectionID = connection.ConnectionId;
                }
                catch(Exception ex)
                {
                    ConnectionID = null;
                    Console.WriteLine(ex.Message);
                }
            }
            return ConnectionID;
        }

        //to maintain the connection ID ...bruh this is really an expensive process !
        public async static Task SendEmployeeCommunicationId()
        {
            try
            {
                await connection.InvokeAsync("SendEmployeeCommunicationId", EmployeeTempData.EmployeeID, ConnectionID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Welp ! this is ..uhmm i don't know I'm just going where the path takes me
        public async static Task SendMessages()
        {
            await connection.SendAsync("ReceivePrivateMessage",);
        }

    }
}
