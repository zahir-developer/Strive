using System;
namespace Strive.Core.ViewModels.Customer
{
    public class VehiclelistViewModel : BaseViewModel
    {
        public string VehicleName = "";
        public VehiclelistViewModel()
        {
        }

        public async void NavigateToGenbook(string vehicleName)
        {
            VehicleName = vehicleName;
            _navigationService.Navigate<GenbookViewModel>();
        }
    }
}
