using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class MembershipSignatureViewModel : BaseViewModel
    {
        
        public MembershipSignatureViewModel()
        {
            AddServiceDetails();
            
        }

        #region Properties



        #endregion Properties


        #region Commands

        public async void NextCommand()
        {
            await _navigationService.Navigate<MyProfileInfoViewModel>();
        }

        public async void BackCommand()
        {
           await _navigationService.Navigate<VehicleAdditionalServiceViewModel>();
        }
        public async Task<bool> AgreeMembership()
        {
            bool agree = true;
            var confirm = await _userDialog.ConfirmAsync("Would you like to create the membership ?");
            if (confirm)
            {
                if (CustomerVehiclesInformation.completeVehicleDetails!=null)
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
        public async Task<bool> CancelMembership()
        {
            var cancelMembership = false;
            var cancel = await _userDialog.ConfirmAsync("Do you want to cancel membership creation?");
            if(cancel)
            {
                cancelMembership = true;
                _userDialog.Toast("Membership creation cancelled");
            }
            else
            {
                cancelMembership = false;
            }
            return cancelMembership;
        }
        public void AddServiceDetails()
        {
            MembershipDetails.
                customerVehicleDetails.
                clientVehicleMembershipModel.
                clientVehicleMembershipService = new List<ClientVehicleMembershipService>();
        }

        public void NoSignatureError()
        {
            _userDialog.Alert("Please sign in the blank space");
        }

        public async Task NavToMembership()
        {
            await _navigationService.Navigate<VehicleMembershipViewModel>();
        }
        #endregion Commands
        
    }
}
