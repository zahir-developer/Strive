using System;
using System.Threading.Tasks;
using Strive.Core.Utils;
using MvvmCross.Plugin.Messenger;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class TermsViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;

        public TermsViewModel()
        {
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 5)
            {
                await _navigationService.Close(this);
                _messageToken.Dispose();
            }
        }

        //public override void ViewDisappeared()
        //{
        //    _messageToken.Dispose();
        //}

        public async Task NavigateBackCommand()
        {
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 5, "exit!"));
        }

        public void NextCommand()
        {
            _navigationService.Navigate<SignatureViewModel>();
        }
    }
}
