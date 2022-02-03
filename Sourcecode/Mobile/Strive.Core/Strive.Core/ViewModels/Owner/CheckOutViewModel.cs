using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Owner
{
    public class CheckOutViewModel : BaseViewModel
    {
        #region Properties

        public CheckoutDetails CheckOutVehicleDetails { get; set; }
        public holdCheckoutResponse holdResponse { get; set; }
        public CheckoutResponse status { get; set; }
        public int locationID { get; set; }
        public List<EmployeeLocation> EmployeeLocations { get; set; }
        public string _selectedLocation;

        public string ItemLocation
        {
            get
            {
                return _selectedLocation;
            }
            set { SetProperty(ref _selectedLocation, value); }
        }
        #endregion Properties

        #region Commands

        public async Task GetCheckOutDetails()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var result = await AdminService.CheckOutVehicleDetails(new GetAllEmployeeDetail_Request
            {
                startDate = (System.DateTime.Now).ToString("yyy-MM-dd"),
                endDate = (System.DateTime.Now).ToString("yyy-MM-dd"),
                locationId = locationID,
                pageNo = 1,
                pageSize = 100,
                query = "",
                sortOrder = "ASC",
                sortBy = "TicketNumber",
                status = true,
            });
            if (result == null || result.GetCheckedInVehicleDetails.checkOutViewModel == null)
            {
                if (CheckOutVehicleDetails != null)
                {
                    CheckOutVehicleDetails = null;
                }
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

        public async Task updateHoldStatus(int Id)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var result = await AdminService.CheckOutHold(new holdCheckoutReq
            {
                id = Id,
                isHold = true,
            });

            if (result != null)
            {
                holdResponse = result;
            }
            _userDialog.HideLoading();
        }

        public async Task updateCompleteStatus(int Id)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var result = await AdminService.CheckOutComplete(new completeCheckoutReq
            {
                jobId = Id,
                actualTimeOut = DateTime.Now,
            });

            if (result != null)
            {
                holdResponse = result;
            }
            _userDialog.HideLoading();
        }

        public async Task DoCheckout(int Id)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var result = await AdminService.DoCheckout(new doCheckoutReq
            {
                jobId = Id,
                checkOut = true,
                checkOutTime = DateTime.Now,
            });

            if (result != null)
            {
                status = result;
            }
            _userDialog.HideLoading();
        }

        #endregion Commands
    }
}
