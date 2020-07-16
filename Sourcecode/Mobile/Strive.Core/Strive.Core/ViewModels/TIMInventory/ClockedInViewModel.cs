using System;
using System.Threading.Tasks;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockedInViewModel : BaseViewModel
    {
        public ClockedInViewModel()
        {
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateClockOutCommand()
        {
            //await _navigationService.Close(this);
            await _navigationService.Navigate<ClockOutViewModel>();
        }
    }
}
