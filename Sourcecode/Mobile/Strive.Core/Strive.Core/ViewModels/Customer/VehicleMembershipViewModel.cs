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
         public MembershipServiceList membershipList { get; set; }
        #endregion Properties

        #region Commands

        private void SetVehicleInformation()
        {
            MembershipDetails.customerVehicleDetails = new ClientVehicleRoot();
            MembershipDetails.customerVehicleDetails.clientVehicle = new ClientVehicle();
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle = new ClientVehicleDetail();
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.clientId = CustomerInfo.ClientID;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.isDeleted = false;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.vehicleMfr = MembershipDetails.vehicleMakeNumber;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.vehicleModel = MembershipDetails.modelNumber;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.vehicleColor = MembershipDetails.colorNumber;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.isDeleted = false;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.isActive = true;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.locationId = 1;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.createdDate = DateTime.Now;
            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle.updatedDate = DateTime.Now;
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
           if(VehicleMembershipCheck())
            {
                await _navigationService.Navigate<VehicleUpchargeViewModel>();
            }
        }

        public async void BackCommand()
        {
            await _navigationService.Navigate<VehicleInfoEditViewModel>();
        }

        public bool VehicleMembershipCheck()
        {
            if(MembershipDetails.selectedMembership == 0)
            {
                _userDialog.Alert("Please choose a membership");
                return false;
            }
            return true;
        }

        #endregion Commands
    }
}
