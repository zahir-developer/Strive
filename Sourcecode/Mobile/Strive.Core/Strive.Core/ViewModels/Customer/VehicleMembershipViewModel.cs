using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleMembershipViewModel : BaseViewModel
    {
        public VehicleMembershipViewModel()
        {
            SetVehicleInformation();
        }
        #region Properties

         public ClientVehicleRoot customerVehicleDetails { get; set; }
         public MembershipServiceList membershipList { get; set; }
        #endregion Properties

        #region Commands

        private void SetVehicleInformation()
        {
            customerVehicleDetails = new ClientVehicleRoot();
            customerVehicleDetails.clientVehicle = new ClientVehicle();
            customerVehicleDetails.clientVehicle.clientVehicle = new ClientVehicleDetail();
            customerVehicleDetails.clientVehicle.clientVehicle.clientId = CustomerInfo.ClientID;
            customerVehicleDetails.clientVehicle.clientVehicle.isDeleted = false;
            customerVehicleDetails.clientVehicle.clientVehicle.vehicleMfr = MembershipDetails.selectedMake;
            customerVehicleDetails.clientVehicle.clientVehicle.vehicleModel = MembershipDetails.selectedModel;
            customerVehicleDetails.clientVehicle.clientVehicle.vehicleColor = MembershipDetails.selectedColor;

        }
        public async Task getMembershipDetails()
        {
            _userDialog.ShowLoading(Strings.Loading);
            membershipList = new MembershipServiceList();
            membershipList = await AdminService.GetMembershipServiceList();
            if (membershipList == null)
            {

            }
            _userDialog.HideLoading();
        }

        public async void NextCommand()
        {
            await _navigationService.Navigate<VehicleUpchargeViewModel>();
        }

        public async void BackCommand()
        {
            await _navigationService.Navigate<VehicleInfoEditViewModel>();
        }


        #endregion Commands
    }
}
