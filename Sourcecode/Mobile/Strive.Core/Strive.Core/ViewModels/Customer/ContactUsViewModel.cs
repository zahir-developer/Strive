using Acr.UserDialogs;
using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class ContactUsViewModel : BaseViewModel
    {

        #region Properties

        public ICarwashLocationService carWashLocationService = Mvx.IoCProvider.Resolve<ICarwashLocationService>();
        public Locations Locations;
        public washLocations locationStatus;

        #endregion Properties

        #region Commands

        public async Task<Locations> GetAllLocationsCommand()
        {
            var washLocations = await carWashLocationService.GetAllCarWashLocations();

            if (washLocations == null)
            {
                Locations = new Locations();

                return Locations;
            }
            else
            {
                Locations = new Locations();
                Locations.Location = new List<Location>();
                foreach (var locations in washLocations.Location)
                {
                    if (locations.Latitude != null && locations.Longitude != null)
                    {
                        Locations.Location.Add(locations);
                    }
                }
                return Locations;
            }
        }

        public async Task<washLocations> GetAllLocationStatus()
        {
            LocationStatusReq request = new LocationStatusReq()
            {
                Date = (System.DateTime.Now).ToString("yyy-MM-dd"),
                LocationId = 0
            };
            var washLocations = await carWashLocationService.GetAllLocationStatus(request);

            if (washLocations == null)
            {
                locationStatus = new washLocations();
                return locationStatus;
            }
            else
            {
                locationStatus = new washLocations();
                locationStatus.Washes = new List<LocationStatus>();
                foreach (var locationItem in washLocations.Washes)
                {
                    if (locationItem.Latitude != null && locationItem.Longitude != null)
                    {
                        locationStatus.Washes.Add(locationItem);
                    }
                }
                return locationStatus;
            }
        }

        public void LogoutCommand()
        {
            var confirmconfig = new ConfirmConfig
            {
                Title = Strings.LogoutTitle,
                Message = Strings.LogoutMessage,
                CancelText = Strings.LogoutCancelButton,
                OkText = Strings.LogoutSuccessButton,
                OnAction = success =>
                {
                    if (success)
                    {
                        CustomerInfo.Clear();
                        _navigationService.Close(this);
                        _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
                    }
                }

            };
            _userDialog.Confirm(confirmconfig);
        }
        #endregion Commands

    }
}
