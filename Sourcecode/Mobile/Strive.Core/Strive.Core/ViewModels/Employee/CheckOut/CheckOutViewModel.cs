using Acr.UserDialogs;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Resources;
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
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var result = await AdminService.CheckOutVehicleDetails(1);
            if (result == null)
            {

            }
            else
            {
                CheckOutVehicleDetails = new CheckOutVehicleDetails();
                CheckOutVehicleDetails.GetCheckedInVehicleDetails = new List<GetCheckedInVehicleDetails>();
                CheckOutVehicleDetails = result;
            }
            _userDialog.HideLoading();
        }

        #endregion Commands
    }
}
