using System;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class RootViewModel : BaseViewModel
    {
        public RootViewModel()
        {
        }

        public async Task ShowClockInCommand()
        {
            await _navigationService.Navigate<ClockInViewModel>();
        }
    }
}
