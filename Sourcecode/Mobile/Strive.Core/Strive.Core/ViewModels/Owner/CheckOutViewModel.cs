using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Owner
{
    public class CheckOutViewModel : BaseViewModel
    {
        #region Properties

        public CheckoutDetails CheckOutVehicleDetails { get; set; }

        #endregion Properties

        #region Commands

        public async Task GetCheckOutDetails()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var result = await AdminService.CheckOutVehicleDetails(new GetAllEmployeeDetail_Request
            {
                startDate = (System.DateTime.Now.AddDays(-1)).ToString("yyy-MM-dd"),
                endDate = (System.DateTime.Now).ToString("yyy-MM-dd"),
                locationId = null,
                pageNo = 1,
                pageSize = null,
                query = "",
                sortOrder = null,
                sortBy = null,
                status = true,
            });
            if (result == null || result.GetCheckedInVehicleDetails.checkOutViewModel == null)
            {
                _userDialog.Toast("No relatable data");
            }
            else
            {
                CheckOutVehicleDetails = new CheckoutDetails();
                CheckOutVehicleDetails.GetCheckedInVehicleDetails = new GetCheckedInVehicleDetails();
                CheckOutVehicleDetails.GetCheckedInVehicleDetails.checkOutViewModel = new List<checkOutViewModel>();
                CheckOutVehicleDetails = result;
            }
            _userDialog.HideLoading();
        }

        #endregion Commands
    }
}
