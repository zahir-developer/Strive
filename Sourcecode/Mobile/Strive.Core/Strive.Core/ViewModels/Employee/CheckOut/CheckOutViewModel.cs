using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.CheckOut
{
    public class CheckOutViewModel : BaseViewModel
    {
        #region Properties

        #endregion Properties

        #region Commands

        public async Task GetCheckOutDetails()
        {
            var result = await AdminService.CheckOutVehicleDetails();
            if (result == null)
            {

            }
            else
            {

            }
        }

        #endregion Commands
    }
}
