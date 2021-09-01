using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class SchedulePreviewDetailsViewModel : BaseViewModel
    {

        public async Task BookNow()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            //prepareDetailSchedule();
            //await AdminService.ScheduleDetail();

            _userDialog.HideLoading();
        }

        public async void NavtoScheduleDate()
        {
            await _navigationService.Navigate<ScheduleAppointmentDateViewModel>();
        }
        public async void NavtoScheduleView()
        {
            await _navigationService.Navigate<ScheduleViewModel>();
        }
        public async void NavtoConfirmSchedule()
        {
            await _navigationService.Navigate<ScheduleConfirmationViewModel>();
        }

        //DetailSchedule  prepareDetailSchedule()
        //{

        //}
    }
}
