using System;
using System.Threading.Tasks;
using MvvmCross;
using Strive.Core.Models.TimInventory;
using Strive.Core.Services.Interfaces;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class WashTimesViewModel : BaseViewModel
    {
        public ILocationService LocationService = Mvx.IoCProvider.Resolve<ILocationService>();

        public Location Location;

        public WashTimesViewModel()
        {
        }

        public async Task<Location> GetAllLocationAddress()
        {
           Location = await LocationService.GetAllLocationAddress();
            _userDialog.HideLoading();
            return Location;
        }
    }
}
