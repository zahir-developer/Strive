using System;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class TermsViewModel : BaseViewModel
    {
        public TermsViewModel()
        {
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public void NextCommand()
        {
            _navigationService.Navigate<SignatureViewModel>();
        }
    }
}
