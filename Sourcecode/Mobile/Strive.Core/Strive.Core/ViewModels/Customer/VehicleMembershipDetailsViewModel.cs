using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleMembershipDetailsViewModel : BaseViewModel
    {
        

        #region Properties

        public string MembershipName { get; set; }
        #endregion Properties



        #region Commands

        public async Task GetMembershipInfo()
        {
            _userDialog.ShowLoading();
           
            var data = await AdminService.GetMembershipServiceList();

            MembershipName = data.Membership.
                            Find(x=> x.MembershipId == CustomerVehiclesInformation.completeVehicleDetails.
                                                        VehicleMembershipDetails.ClientVehicleMembership.MembershipId).MembershipName;

            //_userDialog.HideLoading();
        }

        public async Task CancelMembership()
        {
            _userDialog.ShowLoading();
            var confirm = await _userDialog.ConfirmAsync("Do you wish to cancel the existing membership?");
            if(confirm)
            {
                CustomerVehiclesInformation.membershipDetails = new ClientVehicleRoot();
                CustomerVehiclesInformation.membershipDetails.clientVehicle = new ClientVehicle();
                CustomerVehiclesInformation.membershipDetails.clientVehicle.clientVehicle = null;
                CustomerVehiclesInformation.membershipDetails.clientVehicleMembershipModel = new ClientVehicleMembershipModel();
                CustomerVehiclesInformation.membershipDetails.
                    clientVehicleMembershipModel.clientVehicleMembershipDetails = new ClientVehicleMembershipDetails();
                CustomerVehiclesInformation.membershipDetails.
                    clientVehicleMembershipModel.clientVehicleMembershipService = new List<ClientVehicleMembershipService>();
                MembershipDetails.selectedMembership = 0;
                MembershipDetails.selectedMembershipDetail = null;
                
                GetClientMembershipData();

                GetMembershipServicesData();

                var data = await AdminService.SaveVehicleMembership(CustomerVehiclesInformation.membershipDetails);
                if (data.Status)
                {
                    _userDialog.Toast("Membership has been cancelled");
                }
                else
                {
                    _userDialog.Toast("Membership cancel unsuccessful");
                }
            }
            _userDialog.HideLoading();
        }

        public void GetClientMembershipData()
        {
            // info related to the membership
            CustomerVehiclesInformation.membershipDetails.
                 clientVehicleMembershipModel.clientVehicleMembershipDetails.membershipId = CustomerVehiclesInformation.completeVehicleDetails.
                                                                                            VehicleMembershipDetails.ClientVehicleMembership.MembershipId;

            CustomerVehiclesInformation.membershipDetails.
                 clientVehicleMembershipModel.clientVehicleMembershipDetails.clientVehicleId = CustomerVehiclesInformation.completeVehicleDetails.
                                                                                            VehicleMembershipDetails.ClientVehicleMembership.ClientVehicleId;

            CustomerVehiclesInformation.membershipDetails.
                 clientVehicleMembershipModel.clientVehicleMembershipDetails.locationId = CustomerVehiclesInformation.completeVehicleDetails.
                                                                                            VehicleMembershipDetails.ClientVehicleMembership.LocationId;
            CustomerVehiclesInformation.membershipDetails.
                 clientVehicleMembershipModel.clientVehicleMembershipDetails.clientMembershipId = CustomerVehiclesInformation.completeVehicleDetails.
                                                                                            VehicleMembershipDetails.ClientVehicleMembership.ClientMembershipId;
            CustomerVehiclesInformation.membershipDetails.
              clientVehicleMembershipModel.clientVehicleMembershipDetails.startDate = DateUtils.ConvertDateTimeWithZ();                                                                                    

            CustomerVehiclesInformation.membershipDetails.
               clientVehicleMembershipModel.clientVehicleMembershipDetails.endDate = DateUtils.ConvertDateTimeWithZ();

            CustomerVehiclesInformation.membershipDetails.
                 clientVehicleMembershipModel.clientVehicleMembershipDetails.status = false;
           
            CustomerVehiclesInformation.membershipDetails.
                 clientVehicleMembershipModel.clientVehicleMembershipDetails.isActive = false;
            
            CustomerVehiclesInformation.membershipDetails.
                 clientVehicleMembershipModel.clientVehicleMembershipDetails.isDeleted = true;

        }
        public void GetMembershipServicesData()
        {
            if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembershipService!=null)
            {
                foreach (var data in CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembershipService)
                {
                    ClientVehicleMembershipService serviceView = new ClientVehicleMembershipService();
                    serviceView.clientMembershipId = data.ClientMembershipId;
                    serviceView.clientVehicleMembershipServiceId = data.ClientVehicleMembershipServiceId;
                    serviceView.serviceId = data.ServiceId;
                    serviceView.isActive = false;
                    serviceView.isDeleted = true;
                    serviceView.createdDate = DateUtils.ConvertDateTimeWithZ();
                    serviceView.updatedDate = DateUtils.ConvertDateTimeWithZ();

                    CustomerVehiclesInformation.membershipDetails.
                     clientVehicleMembershipModel.clientVehicleMembershipService.Add(serviceView);
                }
            }
            
        }
        public async void NavigateToDashBoard()
        {
           await _navigationService.Navigate<DashboardViewModel>();
        }
        public async void NavToProfileView()
        {
            await _navigationService.Navigate<MyProfileInfoViewModel>();
        }
        #endregion Commands


    }
}
