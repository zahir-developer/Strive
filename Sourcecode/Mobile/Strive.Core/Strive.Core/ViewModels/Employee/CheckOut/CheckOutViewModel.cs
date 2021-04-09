using Acr.UserDialogs;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
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
            var result = await AdminService.CheckOutVehicleDetails(new GetAllEmployeeDetail_Request
            {
                startDate = null,
                endDate = null,
                locationId = null,
                pageNo = 1,
                pageSize = 100,
                query = "",
                sortOrder = null,
                sortBy = null,
                status = true,
            });
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
