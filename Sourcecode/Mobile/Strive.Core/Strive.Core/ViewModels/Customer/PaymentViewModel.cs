using System;
using System.Collections.Generic;
using System.Linq;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;

namespace Strive.Core.ViewModels.Customer
{
    public class PaymentViewModel : BaseViewModel
    {
        public List<ClientVehicleMembershipService> selectedmembershipServices = new List<ClientVehicleMembershipService>();
       
        
        public PaymentViewModel()
        {

        }
        

        public async void MembershipAgree()
        {
            _userDialog.ShowLoading();
            if (CustomerVehiclesInformation.completeVehicleDetails != null)
            {
                if (CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership != null)
                {
                    var isDeleted = await AdminService.DeleteVehicleMembership(CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.ClientMembershipId);
                }
            }
            PrepareAdditionalServices();
            var data = await AdminService.SaveVehicleMembership(MembershipDetails.customerVehicleDetails);
            if (data.Status == true)
            {
                _userDialog.Toast("Membership has been created successfully");
                MembershipDetails.clearMembershipData();
                await _navigationService.Navigate<MyProfileInfoViewModel>();

            }
            else
            {
                _userDialog.Alert("Error membership not created");
            }
            _userDialog.HideLoading();
        }

        private void PrepareAdditionalServices()
        {
            var servicesDetails = MembershipDetails.completeList.ServicesWithPrice.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
            foreach(var service in servicesDetails)
            {
                ClientVehicleMembershipService clientVehicleMembershipService = new ClientVehicleMembershipService();
                clientVehicleMembershipService.serviceId = service.ServiceId;
                clientVehicleMembershipService.serviceTypeId = service.ServiceTypeId;
                clientVehicleMembershipService.createdDate = DateTime.Now.ToString();
                clientVehicleMembershipService.updatedDate = DateTime.Now.ToString();
                clientVehicleMembershipService.clientMembershipId = service.MembershipId;
                clientVehicleMembershipService.clientVehicleMembershipServiceId = service.MembershipServiceId;
                selectedmembershipServices.Add(clientVehicleMembershipService);
            }

            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel.clientVehicleMembershipService = selectedmembershipServices;
        }
        public enum PaymentStatus
        {
            Success
        }

        public enum PaymentType
        {
            Card,
            Account,
            Tips
        }
    }
}
