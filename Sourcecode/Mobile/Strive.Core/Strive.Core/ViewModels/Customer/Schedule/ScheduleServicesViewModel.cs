using Strive.Core.Models.Customer.Schedule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class ScheduleServicesViewModel : BaseViewModel
    {

        #region Properties
        
        public AvailableScheduleServicesModel scheduleServices { get; set; }

        #endregion Properties

        #region Commands

        public async Task GetScheduledServices()
        {
            var result = await AdminService.GetScheduleServices();
            
            if(result == null)
            {
                _userDialog.Alert("No Services available");
            }
            else
            {
                scheduleServices = new AvailableScheduleServicesModel();
                scheduleServices.ServicesWithPrice = new List<ServicesWithPrice>();
                scheduleServices = result;
            }
        }


        #endregion Commands

    }
}
