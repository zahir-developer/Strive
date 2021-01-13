using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class ScheduleAppointmentDateViewModel : BaseViewModel
    {
        #region Properties
        public AvailableScheduleSlots ScheduleSlotInfo { get; set; }
        #endregion Properties

        #region Commands

        public async Task GetSlotAvailability(int LocationID, string Time)
        {
            var result = await AdminService.GetScheduleSlots(new ScheduleSlotInfo {  locationId = LocationID, date = Time});

            if(result != null)
            {
                ScheduleSlotInfo = new AvailableScheduleSlots();
                ScheduleSlotInfo.GetTimeInDetails = new List<GetTimeInDetails>();
                ScheduleSlotInfo = result;
            }
        }

        #endregion Commands

    }
}
