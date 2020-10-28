using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleInfoViewModel : BaseViewModel
    {

        #region Properties

        public VehicleList vehicleLists { get; set; }
        public ClientVehicleRootView membershipDetails { get; set; }

        #endregion Properties


        #region Commands

        public async Task GetCustomerVehicleList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            vehicleLists = new VehicleList();
            vehicleLists.Status = new List<VehicleDetail>();
            CustomerVehiclesInformation.vehiclesList = new VehicleList();
            CustomerVehiclesInformation.vehiclesList.Status = new List<VehicleDetail>();
            vehicleLists = await AdminService.GetClientVehicle(CustomerInfo.ClientID);
            CustomerVehiclesInformation.vehiclesList = vehicleLists;
            if (vehicleLists == null || vehicleLists.Status.Count == 0)
            {
                _userDialog.Alert("No associated vehicles were found.");
            }

            _userDialog.HideLoading();
        }

        public async Task<bool> DeleteCustomerVehicle(int vehicleID)
        {
            _userDialog.ShowLoading(Strings.Loading);
            bool deleted = false;
            var confirm = await _userDialog.ConfirmAsync("Are you sure you want to delete this vehicle ?");
            if(confirm)
            {
                var data = await AdminService.DeleteCustomerVehicle(vehicleID);
                if (data.Status)
                {
                    _userDialog.Toast("Vehicle details deleted successfully");
                    deleted = true;
                }
                else
                {
                    _userDialog.Toast("Vehicle deletion unsuccessful");
                }
            }
            else
            {
                // closes dialog box
            }
            _userDialog.HideLoading();
            return deleted;
        }

        #endregion Commands


    }
}
