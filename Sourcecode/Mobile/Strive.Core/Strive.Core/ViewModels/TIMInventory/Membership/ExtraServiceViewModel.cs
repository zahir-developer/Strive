using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using System.Linq;
using Strive.Core.Utils;
using MvvmCross.Plugin.Messenger;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class ExtraServiceViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;

        public ObservableCollection<string> ExtraServiceList { get; set; } = new ObservableCollection<string>();

        public ExtraServiceViewModel()
        {
            if(MembershipData.SelectedMembership != null)
            {
                List<string> services = MembershipData.SelectedMembership.Services.Split(',').ToList();
                foreach (var serivce in services)
                {
                    ExtraServiceList.Add(serivce);
                }
            }
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
            _navigationService.Navigate<SelectUpchargeViewModel>();
        }
    }
}
