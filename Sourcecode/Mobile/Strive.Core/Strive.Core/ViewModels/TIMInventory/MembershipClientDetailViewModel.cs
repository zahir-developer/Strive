using System;
using System.Threading.Tasks;
using Strive.Core.Resources;
using Strive.Core.Utils.TimInventory;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Strive.Core.Models.TimInventory;
using System.ComponentModel;
using Strive.Core.ViewModels.TIMInventory.Membership;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class MembershipClientDetailViewModel : BaseViewModel
    {
        public ObservableCollection<VehicleDetail> VehicleList { get; set; } = new ObservableCollection<VehicleDetail>();

        public MembershipClientDetailViewModel()
        {
            if(MembershipData.SelectedClient != null)
            {
                var client = MembershipData.SelectedClient;
                Name = client.FirstName + " " + client.LastName;
                Contact = client.PhoneNumber;
                Email = client.Email;
                GetVehicleList(client.ClientId);
            }
        }

        public string Name { get; set; }

        public string Contact { get; set; }

        public string Email { get; set; }


        async void GetVehicleList(int Id)
        {
            _userDialog.ShowLoading(Strings.Loading);
            var list = await AdminService.GetClientVehicle(Id);
            if(list != null)
            {
                foreach (var item in list.Status)
                    VehicleList.Add(item);
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Navigate<MembershipClientListViewModel>();
        }

        public ObservableCollection<MembershipServices> MembershipServiceList { get; set; } = new ObservableCollection<MembershipServices>();

        public async void GetServiceList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetMembershipServiceList();
            if (result != null)
            {
                MembershipData.MembershipServiceList = result;

                foreach (var item in MembershipData.MembershipServiceList.Membership)
                {
                    MembershipServiceList.Add(item);
                }
            }
            await RaiseAllPropertiesChanged();
        }

        public async Task NavigateToDetailCommand(VehicleDetail vehicle)
        {
            MembershipData.SelectedVehicle = vehicle;
            if(MembershipData.MembershipServiceList == null)
            {
                GetServiceList();
            }
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetVehicleMembership(vehicle.VehicleId);
            if(result != null)
            {
                if (result.VehicleMembershipDetails.ClientVehicleMembership == null)
                {
                    await _navigationService.Navigate<SelectMembershipViewModel>();
                }
                else
                {
                    MembershipData.MembershipDetailView = result.VehicleMembershipDetails;
                    await _navigationService.Navigate<VehicleMembershipDetailViewModel>();
                }
            } 
        }
    }
}
