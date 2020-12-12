﻿using Strive.Core.Models.Customer;
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
            await _navigationService.Navigate<TermsAndConditionsViewModel>();
        }

        public async void BackCommand()
        {
           await _navigationService.Navigate<VehicleAdditionalServiceViewModel>();
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
                clientVehicleMembershipService = new List<Models.TimInventory.ClientVehicleMembershipService>();
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
