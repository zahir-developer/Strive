using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
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
         public modelUpchargeResponse modelUpcharge { get; set; }
        #endregion Properties

        #region Commands

        private void SetVehicleInformation()
        {
            PreviousMembership(CustomerInfo.ClientID);
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
            var result = await AdminService.GetCommonCodes("SERVICETYPE");
            var washId = result.Codes.Find(x => x.CodeValue == "Wash-Upcharge");
            var upchargeRequest = new modelUpcharge()
            {
                upchargeServiceType = washId.CodeId ,
                modelId = MembershipDetails.modelNumber ?? 0
            };

            modelUpcharge = new modelUpchargeResponse();
            modelUpcharge = await AdminService.GetModelUpcharge(upchargeRequest);
            MembershipDetails.modelUpcharge = modelUpcharge;

            membershipList = new MembershipServiceList();
            membershipList = await AdminService.GetMembershipServiceList();
            if (membershipList == null)
            {

            }
            _userDialog.HideLoading();
        }
        public async Task<bool> PreviousMembership(int id)
        {
            var result = await AdminService.GetVehicleDiscountDetail(id);
            if (result.Status == "true")
            {
                _userDialog.Alert("Membership Discount Available !");
                return true;
            }
            else
            {
                return false;
            }
            
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
        public async void NavToUpcharges()
        {
            await _navigationService.Navigate<VehicleUpchargeViewModel>();
        }
        #endregion Commands
    }
}
