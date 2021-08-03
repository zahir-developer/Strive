using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.HubServices;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
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
            if(result == null)
            {

            }
            else
            {
                if(result.Status)
                {

                }
                else
                {
                    _userDialog.Alert("Communication has not been established");
                }
            }
        }

        public void navigateToCreateGroup()
        {
            _navigationService.Navigate<MessengerCreateGroupViewModel>();
        }

        public async Task LogoutCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }
    }
}
