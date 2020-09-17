using System;
using System.Threading.Tasks;
using MvvmCross.Plugin.Messenger;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class RootViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;

        public RootViewModel()
        {
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if(message.Valuea == 1)
            {
               await _navigationService.Close(this);
                _messageToken.Dispose();
            }
        }

        public override void ViewDisappeared()
        {
            _messageToken.Dispose();
        }

        public async Task ShowClockInCommand()
        {
            await _navigationService.Navigate<ClockInViewModel>();
        }
    }
}
