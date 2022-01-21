using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.HubServices;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Strive.Core.Utils;
using MvvmCross.Plugin.Messenger;

namespace Strive.Core.ViewModels.Employee
{
    public class DashboardViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;
        public bool isAndroid = false;
        public DashboardViewModel()
        {
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 1)
            {
                await _navigationService.Close(this);
                _messageToken.Dispose();
            }
        }

        public override void ViewDisappeared()
        {
            _messageToken.Dispose();
        }
        #region Commands

        public async void Logout()
        {
            var confirm = await _userDialog.ConfirmAsync("Do you want to logout ?");
            if(confirm)
            {
                ChatHubMessagingService.StopConnection();
                await SetChatCommunicationDetails();
                EmployeeTempData.ResetAll();
                if (!isAndroid)
                {
                    await _navigationService.Navigate<LoginViewModel>();
                }
                else
                {
                   await _navigationService.Close(this);
                }
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
