﻿using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class ScheduleAppointmentDateViewModel : BaseViewModel
    {
        #region Properties

        public string checkDate { get; set; }
        public DateTime selectedDate { get; set; }
        public DateTime currentDate { get; set; }
        public AvailableScheduleSlots ScheduleSlotInfo { get; set; }
        public int BayID { get; set; } = -1;
        #endregion Properties

        #region Commands

        public async Task GetSlotAvailability(int LocationID, string Time)
        {
            var result = await AdminService.GetScheduleSlots(new ScheduleSlotInfo {  locationId = LocationID, date = Time});

            if(result != null)
            {
                ScheduleSlotInfo = new AvailableScheduleSlots();
                ScheduleSlotInfo.GetTimeInDetails = new List<GetTimeInDetails>();
                string prevSlotTiming = "";
                BayID = result.GetTimeInDetails.First().BayId;
                foreach (var data in result.GetTimeInDetails)
                {
                    if(BayID == data.BayId)
                    {
                        if (!string.Equals(prevSlotTiming, data.TimeIn))
                        {
                            ScheduleSlotInfo.GetTimeInDetails.Add(data);
                            prevSlotTiming = data.TimeIn;
                        }
                    }                    
                }
            }
        }

        public async void NavToSelect_Preview()
        {
            if (checkSelectedTime() && checkSelectedDate())
            {
                await _navigationService.Navigate<SchedulePreviewDetailsViewModel>();
            }
        }

        public bool checkSelectedTime()
        {
            var selected = false;
            if (CustomerScheduleInformation.ScheduleServiceTime != null)
            {
                selected = true;
            }
            else
            {
                _userDialog.Alert("Please select a time to proceed.");
                selected = false;
            }
            return selected;
        }
        public bool checkSelectedDate()
        {
            selectedDate = DateTime.Parse(checkDate);

            currentDate = DateTime.Now;
            var selected = false;
            if (selectedDate.Date >= currentDate.Date && selectedDate.Month >= currentDate.Month && selectedDate.Year >= currentDate.Year)
            {
                selected = true; 
               
            }
            else
            {
                selected = false;
                _userDialog.Alert("Please select a later date.");
            }
            return selected;
        }


        #endregion Commands

    }
}
