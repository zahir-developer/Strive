using System;
using System.Threading.Tasks;
using System.Timers;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class ClockedInViewModel : BaseViewModel
    {
        public ClockedInViewModel()
        {
            //Timer checkForTime = new Timer(15000);
            //checkForTime.Elapsed += new ElapsedEventHandler(checkForTime_Elapsed);
            //checkForTime.Enabled = true;
        }

        void checkForTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            NavigateBackCommand();
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }

        public async Task NavigateClockOutCommand()
        {
            await _navigationService.Navigate<ClockOutViewModel>();
        }
    }
}
