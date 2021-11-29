using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class TermsAndConditionsViewModel : BaseViewModel
    {

        public TermsAndConditionsViewModel()
        {
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel = new ClientVehicleMembershipModel();

            MembershipDetails.customerVehicleDetails.
                clientVehicleMembershipModel.
                clientVehicleMembershipDetails = new ClientVehicleMembershipDetails();

            MembershipDetails.customerVehicleDetails.clientVehicle.clientVehicle = null;

            SetupMembership();
            SetupServices();
        }

        public void SetupServices()
        {
            ClientVehicleMembershipService clientVehicleAddServices ;
            MembershipDetails.customerVehicleDetails
                .clientVehicleMembershipModel
                .clientVehicleMembershipService = new List<ClientVehicleMembershipService>();
            foreach (var data in MembershipDetails.filteredList.ServicesWithPrice)
            {
                if(MembershipDetails.selectedAdditionalServices.Contains(data.ServiceId) || data.ServiceId == MembershipDetails.selectedUpCharge)
                {
                    clientVehicleAddServices = new ClientVehicleMembershipService();
                    clientVehicleAddServices.clientVehicleMembershipServiceId = 0;
                    //if (CustomerVehiclesInformation.completeVehicleDetails != null)
                    //{
                    //    clientVehicleAddServices.clientMembershipId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.ClientMembershipId;
                    //}
                    //else
                    //{
                        clientVehicleAddServices.clientMembershipId = 0;
                    //}
                    clientVehicleAddServices.serviceId = data.ServiceId;
                    clientVehicleAddServices.serviceTypeId = data.ServiceTypeId;
                    clientVehicleAddServices.isActive = true;
                    clientVehicleAddServices.isDeleted = false;
                    clientVehicleAddServices.createdDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");
                    clientVehicleAddServices.updatedDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");


                    MembershipDetails.customerVehicleDetails
                 .clientVehicleMembershipModel
                 .clientVehicleMembershipService.Add(clientVehicleAddServices);
                }
            }
        }

        public void SetupMembership()
        {
            //if (CustomerInfo.actionType == 2)
            //{
            //    MembershipDetails.customerVehicleDetails
            //    .clientVehicleMembershipModel
            //    .clientVehicleMembershipDetails.clientMembershipId = CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.ClientMembershipId;
            //}
            //else
            //{
                MembershipDetails.customerVehicleDetails
                .clientVehicleMembershipModel
                .clientVehicleMembershipDetails.clientMembershipId = 0;
            //}                

            MembershipDetails.customerVehicleDetails
               .clientVehicleMembershipModel
               .clientVehicleMembershipDetails.clientVehicleId = MembershipDetails.clientVehicleID;

            MembershipDetails.customerVehicleDetails
                .clientVehicleMembershipModel
                .clientVehicleMembershipDetails.locationId = 1;

            MembershipDetails.customerVehicleDetails
                .clientVehicleMembershipModel
                .clientVehicleMembershipDetails.membershipId = MembershipDetails.selectedMembership;

            MembershipDetails.customerVehicleDetails
               .clientVehicleMembershipModel
               .clientVehicleMembershipDetails.status = true;

            MembershipDetails.customerVehicleDetails
              .clientVehicleMembershipModel
              .clientVehicleMembershipDetails.isActive = true;

            //if (CustomerInfo.actionType == 2)
            //{
            //    MembershipDetails.customerVehicleDetails
            //  .clientVehicleMembershipModel
            //  .clientVehicleMembershipDetails.isDeleted = true;
            //}
            //else
            //{
                MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel
              .clientVehicleMembershipDetails.isDeleted = false;
            //}               

            MembershipDetails.customerVehicleDetails
               .clientVehicleMembershipModel
               .clientVehicleMembershipDetails.startDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");

            MembershipDetails.customerVehicleDetails
               .clientVehicleMembershipModel
               .clientVehicleMembershipDetails.endDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");

            MembershipDetails.customerVehicleDetails
              .clientVehicleMembershipModel
              .clientVehicleMembershipDetails.createdDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");

            MembershipDetails.customerVehicleDetails
              .clientVehicleMembershipModel
              .clientVehicleMembershipDetails.updatedDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");
        }

        #region Properties



        #endregion Properties

        #region Commands

        public async Task<bool> AgreeMembership()
        {
            bool agree = true;
            var confirm = await _userDialog.ConfirmAsync("Would you like to create the membership ?");
            if(confirm)
            {
                if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null)
                {
                    var isDeleted = await AdminService.DeleteVehicleMembership(CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.ClientMembershipId);
                }
                var data = await AdminService.SaveVehicleMembership(MembershipDetails.customerVehicleDetails);
                if (data.Status == true)
                {
                    _userDialog.Toast("Membership has been created successfully");
                    MembershipDetails.clearMembershipData();
                    return agree;
                }
                else
                {
                    _userDialog.Alert("Error membership not created");
                    agree = false;
                }
            }
            else
            {
                agree = false;
            }
            return agree;
        }
        public async Task<bool> DisagreeMembership()
        {
            bool disagree = true;
            var confirm = await _userDialog.ConfirmAsync("Do you wish to cancel ?");
            if (confirm)
            {
                MembershipDetails.clearMembershipData();
                _userDialog.Toast("Membership not created");
                return disagree;
            }
            else
            {
                disagree = false;
            }
            return disagree;
        }
        public async void NavigateToLanding()
        {
            await _navigationService.Navigate<MyProfileInfoViewModel>();
        }
        public async void NavToSignatureView()
        {
            foreach (var item in MembershipDetails.selectedAdditionalServices)
            {
                MembershipDetails.filteredList.ServicesWithPrice.Add(MembershipDetails.completeList.ServicesWithPrice.Find(a => a.ServiceId == item));
            }

            await _navigationService.Navigate<MembershipSignatureViewModel>();
        }
        public async void BackCommand()
        {
            await _navigationService.Navigate<MembershipSignatureViewModel>();
        }
        #endregion Commands

    }
}
