using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class ScheduleConfirmationViewModel : BaseViewModel
    {
        public void ClearScheduleData()
        {
            CustomerScheduleInformation.ClearScheduleData();
        }

        public async void NavtoSchedule()
        {
            await _navigationService.Navigate<ScheduleViewModel>();
        }

        public async void BookCarWash()
        {



        }
    }
}
