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

        private void SetupServices()
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
                    clientVehicleAddServices.clientMembershipId = 0;
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

        private void SetupMembership()
        {
            MembershipDetails.customerVehicleDetails
                .clientVehicleMembershipModel
                .clientVehicleMembershipDetails.clientMembershipId = 0;

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

            MembershipDetails.customerVehicleDetails
               .clientVehicleMembershipModel
               .clientVehicleMembershipDetails.isDeleted = false;

            MembershipDetails.customerVehicleDetails
               .clientVehicleMembershipModel
               .clientVehicleMembershipDetails.startDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");

            MembershipDetails.customerVehicleDetails
               .clientVehicleMembershipModel
               .clientVehicleMembershipDetails.endDate = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss.fff'Z'");
        }

        #region Properties



        #endregion Properties

        #region Commands

        public async Task AgreeMembership()
        {
            var data = await AdminService.SaveVehicleMembership(MembershipDetails.customerVehicleDetails);
            if (data.Status == true)
            {
                _userDialog.Toast("Membership has been created successfully");
                return;
            }
            else
            {
                _userDialog.Alert("Error membership not created");
            }
        }
        public void DisagreeMembership()
        {

        }
        public async void NavigateToLanding()
        {
            await _navigationService.Navigate<MyProfileInfoViewModel>();
        }
       
        #endregion Commands

    }
}
