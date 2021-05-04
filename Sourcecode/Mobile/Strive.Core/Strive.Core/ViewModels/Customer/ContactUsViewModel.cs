using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Services.Interfaces;
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


        #endregion Commands

    }
}
