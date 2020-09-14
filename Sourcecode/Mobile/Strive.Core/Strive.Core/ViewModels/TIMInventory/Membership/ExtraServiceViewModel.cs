using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using System.Linq;
using Strive.Core.Utils;
using MvvmCross.Plugin.Messenger;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class ExtraServiceViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;

        public ObservableCollection<ServiceDetail> TotalServiceList { get; set; } = new ObservableCollection<ServiceDetail>();

        public ObservableCollection<ServiceDetail> MembershipServiceList { get; set; } = new ObservableCollection<ServiceDetail>();

        public ObservableCollection<ServiceDetail> ExtraServiceList { get; set; } = new ObservableCollection<ServiceDetail>();

        public ExtraServiceViewModel()
        {
            GetServices();
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        async void GetServices()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var services = await AdminService.GetVehicleServices();
            if(services != null)
            {
                foreach(var service in services.ServicesWithPrice)
                {
                    TotalServiceList.Add(service);
                }
            }
            if (MembershipData.SelectedMembership != null)
            {
                _userDialog.ShowLoading(Strings.Loading);
                var SelectedServices = await AdminService.GetSelectedMembershipServices(MembershipData.SelectedMembership.MembershipId);
                foreach (var serivce in SelectedServices.MembershipDetail)
                {
                    var item = TotalServiceList.Where(s => s.ServiceId == serivce.ServiceId).FirstOrDefault();
                    if(item != null)
                    {
                        TotalServiceList.Remove(item);
                        TotalServiceList.Insert(0, item);
                    }
                    MembershipServiceList.Add(serivce);
                }
            }
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 5)
            {
                await _navigationService.Close(this);
                _messageToken.Dispose();
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public void NextCommand()
        {
            MembershipData.ExtraServices = new List<ClientVehicleMembershipService>();

            int ClientMembership = 0;
            if(MembershipData.MembershipDetailView != null)
            {
                ClientMembership = MembershipData.MembershipDetailView.ClientMembershipId;
            }

            foreach (var service in MembershipServiceList)
            {
                MembershipData.ExtraServices.Add(new ClientVehicleMembershipService()
                {
                    clientVehicleMembershipServiceId = 0,
                    clientMembershipId = ClientMembership,
                    serviceId = service.ServiceId,
                    serviceTypeId = 0,
                    isActive = true,
                    isDeleted = false
                });
            }

            foreach (var service in ExtraServiceList)
            {
                MembershipData.ExtraServices.Add(new ClientVehicleMembershipService()
                {
                    clientVehicleMembershipServiceId = 0,
                    clientMembershipId = ClientMembership,
                    serviceId = service.ServiceId,
                    serviceTypeId = 0,
                    isActive = true,
                    isDeleted = false
                });
            }
            _navigationService.Navigate<TermsViewModel>();
        }
    }
}
