using Strive.Core.Models.Employee.Messenger;
using Strive.Core.Services.HubServices;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.Schedule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee
{
    public class MessengerViewModel : BaseViewModel
    {

        public void navigateToCreateGroup()
        {
            _navigationService.Navigate<MessengerCreateGroupViewModel>();
        }
        public void navigateToChecklist()
        {
            _navigationService.Navigate<ScheduleViewModel>();
        }

        
        public async Task LogoutCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }
    }
}
