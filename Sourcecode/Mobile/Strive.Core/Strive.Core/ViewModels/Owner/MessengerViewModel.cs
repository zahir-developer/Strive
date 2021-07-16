using System;
using System.Threading.Tasks;
using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.HubServices;
using Strive.Core.Utils.Employee;

namespace Strive.Core.ViewModels.Owner
{
    public class MessengerViewModel : BaseViewModel
    {
        public async Task<string> StartCommunication()
        {
            var ConnectionID = await ChatHubMessagingService.StartConnection();
            return ConnectionID;
        }

        public async Task SetChatCommunicationDetails(string commID)
        {
            var communicationData = new ChatCommunication()
            {
                communicationId = commID,
                employeeId = EmployeeTempData.EmployeeID
            };
            var result = await MessengerService.ChatCommunication(communicationData);
            if (result == null)
            {

            }
            else
            {
                if (result.Status)
                {

                }
                else
                {
                    _userDialog.Alert("Communication has not been established");
                }
            }
        }
    }
}
