using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

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

        public void BackCommand()
        {

        }

        private void AddServiceDetails()
        {
            MembershipDetails.
                customerVehicleDetails.
                clientVehicleMembershipModel.
                clientVehicleMembershipService = new List<Models.TimInventory.ClientVehicleMembershipService>();
        }

        #endregion Commands
    }
}
