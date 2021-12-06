using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using MvvmCross.Plugin.Messenger;
using System.Linq;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.Customer;

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
                //_messageToken.Dispose();
            }
        }

        public async void GetServiceList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetMembershipServiceList();
            if(result != null)
            {
                MembershipData.MembershipServiceList = result;

                foreach (var item in MembershipData.MembershipServiceList.Membership)
                {
                    MembershipServiceList.Add(item);
                }
            }
            if(MembershipData.MembershipDetailView != null)
            {
                var SelectedMembership = MembershipServiceList.Where(m => m.MembershipId == MembershipData.MembershipDetailView.ClientVehicleMembership.MembershipId).FirstOrDefault();
                if (SelectedMembership != null)
                {
                    MembershipServiceList.Remove(SelectedMembership);
                    MembershipServiceList.Insert(0, SelectedMembership);
                }
            }
            await RaiseAllPropertiesChanged();
        }
        public async Task getMembershipDetails()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetCommonCodes("SERVICETYPE");
            var washId = result.Codes.Find(x => x.CodeValue == "Wash-Upcharge");
            var upchargeRequest = new modelUpcharge()
            {
                upchargeServiceType = washId.CodeId,
                modelId = MembershipData.SelectedVehicle.VehicleModelId
            };

           var modelUpcharge = new modelUpchargeResponse();
            modelUpcharge = await AdminService.GetModelUpcharge(upchargeRequest);
            MembershipDetails.modelUpcharge = modelUpcharge;

            _userDialog.HideLoading();
        }

        public void NextCommand()
        {
            if(MembershipData.SelectedMembership == null)
            {
                _userDialog.Alert("Please select a membership to continue.");
                return;
            }
            _navigationService.Navigate<SelectUpchargeViewModel>();
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }
    }
}
