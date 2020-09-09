using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using MvvmCross.Plugin.Messenger;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SelectMembershipViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;

        public ObservableCollection<MembershipServices> MembershipServiceList { get; set; } = new ObservableCollection<MembershipServices>();

        public SelectMembershipViewModel()
        {
            GetServiceList();
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

        public async void GetServiceList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetMembershipServiceList();
            if(result != null)
            {
                MembershipData.MembershipServiceList = result;
            }
            if(MembershipData.MembershipServiceList != null)
            {
                foreach (var item in MembershipData.MembershipServiceList.Membership)
                {
                    MembershipServiceList.Add(item);
                }
            }
            RaiseAllPropertiesChanged();
        }

        public void NextCommand()
        {
            if(MembershipData.SelectedMembership == null)
            {
                _userDialog.Alert("Please select a membership to continue.");
                return;
            }
            _navigationService.Navigate<ExtraServiceViewModel>();
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }
    }
}
