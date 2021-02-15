using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.CheckOut
{
    public class CheckOutViewModel : BaseViewModel
    {
        #region Properties

        public CheckOutVehicleDetails CheckOutVehicleDetails { get; set; }
        #endregion Properties

        #region Commands

        public async Task GetCheckOutDetails()
        {
            var result = await AdminService.CheckOutVehicleDetails(EmployeeTempData.EmployeeID);
            if (result == null)
            {

            }
            else
            {
                CheckOutVehicleDetails = new CheckOutVehicleDetails();
                CheckOutVehicleDetails.GetCheckedInVehicleDetails = new List<GetCheckedInVehicleDetails>();
                CheckOutVehicleDetails = result;
            }
        }

        #endregion Commands
    }
}
