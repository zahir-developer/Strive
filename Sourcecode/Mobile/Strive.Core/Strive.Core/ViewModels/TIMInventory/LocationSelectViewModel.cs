using System;
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
                roleId = 5,
                date = DateUtils.GetTodayDateString()
            };
            var status = await AdminService.GetClockInStatus(request);
            if (status.TimeClock.Count > 0)
            {
                var SingleTimeClock = new TimeClockRoot();
                SingleTimeClock.TimeClock = status.TimeClock[0];
                EmployeeData.ClockInStatus = SingleTimeClock;
            }
            await _navigationService.Navigate<RootViewModel>();
            _navigationService.Close(this);
            await RaiseAllPropertiesChanged();
        }
    }
}
