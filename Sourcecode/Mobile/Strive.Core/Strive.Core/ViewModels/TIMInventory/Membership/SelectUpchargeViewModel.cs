using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Strive.Core.Utils;
using MvvmCross.Plugin.Messenger;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SelectUpchargeViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;

        public ObservableCollection<string> UpchargesList { get; set; } = new ObservableCollection<string>();

        public SelectUpchargeViewModel()
        {
            UpchargesList.Add("None");
            UpchargesList.Add("A-$15/75");
            UpchargesList.Add("A-$15/150");
            UpchargesList.Add("C-$25/300");
            UpchargesList.Add("D-$35/500");
            UpchargesList.Add("E-$50/600");
            RaiseAllPropertiesChanged();
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
            await _navigationService.Close(this);
        }

        public void NextCommand()
        {
            _navigationService.Navigate<TermsViewModel>();
        }
    }
}
