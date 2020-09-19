using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class MembershipSignatureViewModel : BaseViewModel
    {

        #region Commands

        public async void NextCommand()
        {
           await _navigationService.Navigate<TermsAndConditionsViewModel>();
        }

        public void BackCommand()
        {

        }

        #endregion Commands
    }
}
