using Strive.Core.Models.Customer;

namespace Strive.Core.ViewModels.Customer
{
    public class PaymentViewModel : BaseViewModel
    {
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
