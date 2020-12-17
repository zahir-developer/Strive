using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleAdditionalServiceViewModel : BaseViewModel
    {
        public VehicleAdditionalServiceViewModel()
        {
           
            AddUpchargesToServiceList();
        }

        #region Properties


        #endregion Properties

        #region Commands

        public void AddUpchargesToServiceList()
        {

        }

        public async void NavToSignatureView()
        {
            await _navigationService.Navigate<MembershipSignatureViewModel>();
        }
        #endregion Commands
    }
}
