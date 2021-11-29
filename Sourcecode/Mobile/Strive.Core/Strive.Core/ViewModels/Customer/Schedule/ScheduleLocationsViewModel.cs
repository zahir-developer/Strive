using Acr.UserDialogs;
using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using Strive.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class ScheduleLocationsViewModel : BaseViewModel
    {

        #region Properties

        public ICarwashLocationService carWashLocationService = Mvx.IoCProvider.Resolve<ICarwashLocationService>();
        public Locations Locations;

        #endregion Properties

        #region Commands

        public async Task GetAllLocationsCommand()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var washLocations = await carWashLocationService.GetAllCarWashLocations();

            if (washLocations == null)
            {
                Locations = new Locations();
            }
            else
            {
                Locations = new Locations();
                Locations = washLocations;
            }
            _userDialog.HideLoading();
        }
        public async void NavToSelect_Service()
        {
            if (checkSelectedLocation())
            {
                await _navigationService.Navigate<ScheduleServicesViewModel>();
                
            }
        }
        public async void NavToSchedule()
        {
            await _navigationService.Navigate<ScheduleViewModel>();
        }

        public bool checkSelectedLocation()
        {
            var selected = false;
            if (CustomerScheduleInformation.ScheduleLocationCode != -1)
            {
                selected = true;
            }
            else
            {
                _userDialog.Alert("Please select a location to proceed.");
                selected = false;
            }
            return selected;
        }

        #endregion Commands

    }
}
