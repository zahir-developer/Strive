using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.HubServices;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class DashboardViewModel : BaseViewModel
    {



        #region Commands
        
        public async void Logout()
        {
            var confirm = await _userDialog.ConfirmAsync("Do you want to logout ?");
            if(confirm)
            {
                ChatHubMessagingService.StopConnection();
                await SetChatCommunicationDetails();
                EmployeeTempData.ResetAll();
                await _navigationService.Close(this);
            }
        }
        public async Task SetChatCommunicationDetails()
        {
            var communicationData = new ChatCommunication()
            {
                communicationId = "0",
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
        #endregion Commands


    }
}
