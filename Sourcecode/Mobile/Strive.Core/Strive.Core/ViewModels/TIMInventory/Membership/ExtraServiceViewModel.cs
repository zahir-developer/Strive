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
using Strive.Core.Models.Customer.Schedule;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class ExtraServiceViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;
       
        public ObservableCollection<ServiceDetail> TotalServiceList { get; set; } = new ObservableCollection<ServiceDetail>();

        public ObservableCollection<ServiceDetail> MembershipServiceList { get; set; } = new ObservableCollection<ServiceDetail>();

        public ObservableCollection<AllServiceDetail> ExtraServiceList { get; set; } = new ObservableCollection<AllServiceDetail>();

        public ObservableCollection<AllServiceDetail> serviceList { get; set; } = new ObservableCollection<AllServiceDetail>();

        public ExtraServiceViewModel()
        {
            GetAdditionalServices();
            //GetServices();
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        public async void GetAdditionalServices()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetScheduleServices(EmployeeData.selectedLocationId);

            if(result != null)
            {
                foreach(var item in result.AllServiceDetail)
                {
                    if(item.ServiceTypeName == "Additional Services")
                    {
                        serviceList.Add(item);
                    }
                }
            }
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
                //_messageToken.Dispose();
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public void NextCommand()
        {
            MembershipData.ExtraServices = new List<ClientVehicleMembershipService>();

            var SelectedMembership = MembershipData.MembershipServiceList.Membership.Where(m => m.MembershipId == MembershipData.SelectedMembership.MembershipId).FirstOrDefault();

            string[] selectedServices = SelectedMembership.Services.Split(",");

            int ClientMembership = 0;
            if(MembershipData.MembershipDetailView != null)
            {
                ClientMembership = MembershipData.MembershipDetailView.ClientVehicleMembership.ClientMembershipId;
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
                    isDeleted = false,
                    
                }); 
            }

            //if(ExtraServiceList.Count == 0)
            //{
            //    _userDialog.AlertAsync("Select a service to proceed further");
            //}
            //else
            //{
            foreach (var service in ExtraServiceList)
            {
                MembershipData.ExtraServices.Add(new ClientVehicleMembershipService()
                {
                    clientVehicleMembershipServiceId = 0,
                    clientMembershipId = ClientMembership,
                    serviceId = service.ServiceId,
                    serviceTypeId = 0,
                    isActive = true,
                    isDeleted = false,
                    services = selectedServices.Count() > 0 ? selectedServices.Last() : ""
                });
            }
            _navigationService.Navigate<TermsViewModel>();
            //}           
        }
    }
}
