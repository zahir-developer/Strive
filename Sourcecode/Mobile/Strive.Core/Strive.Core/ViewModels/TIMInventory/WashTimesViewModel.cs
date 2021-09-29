using System;
using System.Threading.Tasks;
using MvvmCross;
using Strive.Core.Models.TimInventory;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;
using System.Collections.Generic;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class WashTimesViewModel : BaseViewModel
    {
        public ILocationService LocationService = Mvx.IoCProvider.Resolve<ILocationService>();
        public Locations Location;

        //public ICarwashLocationService carWashLocationService = Mvx.IoCProvider.Resolve<ICarwashLocationService>();
        //public washLocations locationStatus;

        public WashTimesViewModel()
        {
        }

        public async Task<Locations> GetAllLocationAddress()
        {
            //_userDialog.ShowLoading(Strings.Loading);
            var locations = await LocationService.GetAllLocationAddress();

            if (locations == null)
            {
                Location = new Locations();

                return Location;
            }
            else
            {
                Location = new Locations();
                Location.Location = new List<LocationAddress>();
                foreach (var item in locations.Location)
                {
                    if (item.Latitude != null && item.Longitude != null)
                    {
                        Location.Location.Add(item);
                    }
                }
                return Location;
            }
        }

        //public async Task<washLocations> GetAllLocationStatus()
        //{
        //    LocationStatusReq request = new LocationStatusReq()
        //    {
        //        Date = (System.DateTime.Now).ToString("yyy-MM-dd"),
        //        LocationId = 0
        //    };
        //    var washLocations = await carWashLocationService.GetAllLocationStatus(request);

        //    if (washLocations == null)
        //    {
        //        locationStatus = new washLocations();
        //        return locationStatus;
        //    }
        //    else
        //    {
        //        locationStatus = new washLocations();
        //        locationStatus.Washes = new List<LocationStatus>();
        //        foreach (var locationItem in washLocations.Washes)
        //        {
        //            if (locationItem.Latitude != null && locationItem.Longitude != null)
        //            {
        //                locationStatus.Washes.Add(locationItem);
        //            }
        //        }
        //        return locationStatus;
        //    }
        //}

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }
    }
}
