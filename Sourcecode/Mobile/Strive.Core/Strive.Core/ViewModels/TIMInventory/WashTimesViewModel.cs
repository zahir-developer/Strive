using System;
using System.Threading.Tasks;
using MvvmCross;
using Strive.Core.Models.TimInventory;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;
using System.Collections.Generic;

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
            Location = new Location()
            {
                LocationAddress = new List<LocationAddress>()
                {
                    new LocationAddress(){
                        OpenTime = "09:30 AM",
                    CloseTime = "05:00 PM",
                    WashTiming = "40",
                    Latitude = 34.070915,
                    Longitude = -84.295814,
                    Address1 = "Old Milton"
                    },
                    new LocationAddress(){
                        OpenTime = "11:00 AM",
                    CloseTime = "07:00 PM",
                    WashTiming = "25",
                    Latitude = 33.967986,
                    Longitude = -84.257494,
                    Address1 = "Halcomb Bridge",
                    },
                     new LocationAddress(){
                        OpenTime = "08:00 AM",
                    CloseTime = "04:00 PM",
                    WashTiming = "08:00 AM",
                    Latitude = 34.068472,
                    Longitude = -84.300668,
                    Address1 = "Main Street",
                    }
                }
            };
            return Location;
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }
    }
}
