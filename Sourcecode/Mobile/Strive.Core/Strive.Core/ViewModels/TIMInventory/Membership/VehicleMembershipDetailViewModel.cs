using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvvmCross.Plugin.Messenger;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class VehicleMembershipDetailViewModel : BaseViewModel
    {

        private MvxSubscriptionToken _messageToken;

        public VehicleMembershipDetailViewModel()
        {
            GetDetails();
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }


        void GetDetails()
        {
            MembershipName = MembershipData.MembershipServiceList.Membership.Where(m => m.MembershipId == MembershipDetail.MembershipId).Select(m => m.MembershipName).FirstOrDefault();
        }

        public ClientVehicleMembershipView MembershipDetail { get; set; } = MembershipData.MembershipDetailView.ClientVehicleMembership;

        public string MembershipName { get; set; }

        public string ActivatedDate { get { return MembershipDetail.StartDate.ToShortDateString(); } set { } }

        public string CancelledDate { get { return MembershipDetail.EndDate.ToShortDateString(); } set { } }

        public string Status { get { return MembershipDetail.Status ? "Active" : "Inactive"; } set { } }

        public async Task NavigateBackCommand()
        {
            MembershipData.MembershipDetailView = null;
            await _navigationService.Close(this);
        }

        public async Task ChangeMembershipCommand()
        {
            
            await _navigationService.Navigate<SelectMembershipViewModel>();
        }

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 5)
            {
                await _navigationService.Close(this);
                _messageToken.Dispose();
            }
        }

        public async Task CancelMembershipCommand()
        {
            var affirmative = await _userDialog.ConfirmAsync("Are you sure want to cancel the membership?", "Cancel", "Yes", "No");
            if(!affirmative)
            {
                return;
            }
            var VehicleMembership = PrepareVehicleMembership();
            _userDialog.ShowLoading("Cancelling the membership");
            var result = await AdminService.SaveVehicleMembership(VehicleMembership);
            if (result == null)
            {
                await _userDialog.AlertAsync("Error occured");
                return;
            }
            if (result.Status)
            {
                await _userDialog.AlertAsync("Membership Cancelled");
            }
            else
            {
                await _userDialog.AlertAsync("Could not cancel membership");
            }
            await _navigationService.Close(this);
            MembershipData.MembershipDetailView = null;

        }


        ClientVehicleRoot PrepareVehicleMembership()
        {
            int ClientMembership = 0;
            if (MembershipData.MembershipDetailView != null)
            {
                ClientMembership = MembershipData.MembershipDetailView.ClientVehicleMembership.ClientMembershipId;
            }
            var model = new ClientVehicleRoot()
            {
                clientVehicle = new ClientVehicle() { clientVehicle = null },
                clientVehicleMembershipModel = new ClientVehicleMembershipModel()
                {
                    clientVehicleMembershipDetails = new ClientVehicleMembershipDetails()
                    {
                        clientMembershipId = ClientMembership,
                        clientVehicleId = MembershipData.SelectedVehicle.VehicleId,
                        locationId = EmployeeData.selectedLocationId,
                        membershipId = MembershipData.MembershipDetailView.ClientVehicleMembership.MembershipId,
                        startDate = DateUtils.GetTodayDateString(),
                        endDate = DateUtils.GetTodayDateString(),
                        status = true,
                        notes = "",
                        isActive = false,
                        isDeleted = true
                    },
                    clientVehicleMembershipService = new List<ClientVehicleMembershipService>()
                    {
                    new ClientVehicleMembershipService()
                    {
                        clientVehicleMembershipServiceId = 0,
                        clientMembershipId = ClientMembership,
                        serviceId = MembershipData.MembershipDetailView.ClientVehicleMembershipService[0].ServiceId,
                        serviceTypeId = 0,
                        isActive = true,
                        isDeleted = false
                    }
                    },
                }
            };
            return model;
        }
    }
}
