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
                Locations = washLocations;
                return Locations;
            }
        }


        #endregion Commands

    }
}
