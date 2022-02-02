using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using System.Collections.Generic;
using MvvmCross.Plugin.Messenger;
using System.Linq;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.Customer;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SignatureViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;
        
        public  static string Base64ContractString;
        public SignatureViewModel()
        {
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 5)
            {
                await _navigationService.Close(this);
                //_messageToken.Dispose();
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }
        public async void NextCommand()
        {
             await _navigationService.Navigate<PaymentViewModel>();
        }


    }
}
