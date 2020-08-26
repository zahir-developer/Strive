using System;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SignatureViewModel : BaseViewModel
    {
        public SignatureViewModel()
        {
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public void NextCommand()
        {
            
        }
    }
}
