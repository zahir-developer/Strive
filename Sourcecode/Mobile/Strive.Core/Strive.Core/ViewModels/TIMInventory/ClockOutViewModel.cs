using System;
using System.Threading.Tasks;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockOutViewModel : BaseViewModel
    {
        public ClockOutViewModel()
        {
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }
    }
}
