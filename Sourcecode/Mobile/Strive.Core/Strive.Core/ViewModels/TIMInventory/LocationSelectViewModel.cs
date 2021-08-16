﻿using System;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class LocationSelectViewModel : BaseViewModel
    {
        public LocationSelectViewModel()
        {
        }

        public string ItemLocation
        {
            get
            {
                return _selectedLocation;
            }
            set { SetProperty(ref _selectedLocation, value); }
        }

         
        public string _selectedLocation;

        public void locationCommand(EmployeeLocation location)
        {
            ItemLocation = location.LocationName;
            //EmployeeData.selectedLocationId = location.LocationId;
            EmployeeData.selectedLocationId = EmployeeData.EmployeeDetails.EmployeeLocations[0].LocationId;
        }

        public  async void NextScreen()
        {
            var request = new TimeClockRequest()
            {
                locationId = EmployeeData.selectedLocationId,
                employeeId = EmployeeData.EmployeeDetails.EmployeeLogin.EmployeeId,
                roleId = 0,
                date = DateUtils.GetTodayDateString()
            };
            var status = await AdminService.GetClockInStatus(request);
            if (status.timeClock.Count > 0)
            {
                var SingleTimeClock = new TimeClockRoot();
                foreach (var item in status.timeClock)
                {                    
                    if (item.outTime == null)
                    {
                        SingleTimeClock.TimeClock = item;
                        EmployeeData.ClockInStatus = SingleTimeClock;
                        var inTime = EmployeeData.ClockInStatus.TimeClock.inTime.Substring(0, 19);
                        EmployeeData.ClockInTime = inTime;
                        await _navigationService.Navigate<ClockedInViewModel>();
                        _navigationService.Close(this);
                    }                 
                }
                await _navigationService.Navigate<RootViewModel>();
            }
            else
            {
                await _navigationService.Navigate<RootViewModel>();
            }
            _navigationService.Close(this);
            await RaiseAllPropertiesChanged();
        }
    }
}
