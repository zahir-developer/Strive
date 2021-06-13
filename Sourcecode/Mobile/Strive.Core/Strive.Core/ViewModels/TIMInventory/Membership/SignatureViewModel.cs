using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using System.Collections.Generic;
using MvvmCross.Plugin.Messenger;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SignatureViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;

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
            var VehicleMembership = PrepareVehicleMembership();
            _userDialog.ShowLoading("Assigning membership to the vehicle...");
            var result = await AdminService.SaveVehicleMembership(VehicleMembership);
            if(result == null)
            {
                await _userDialog.AlertAsync("Membership not Assigned. Please try again.");
                _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 5, "exit!"));
                return;
            }
            if(result.Status)
            {
                await _userDialog.AlertAsync("Membership Assigned");
            }
            else
            {
                await _userDialog.AlertAsync("Membership not Assigned. Please try again.");
            }
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 5, "exit!"));
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
                        membershipId = MembershipData.SelectedMembership.MembershipId,
                        startDate = DateUtils.GetTodayDateString(),
                        endDate = DateUtils.GetTodayDateString(),
                        status = true,
                        notes = "",
                        isActive = true,
                        isDeleted = false
                    },
                    clientVehicleMembershipService = MembershipData.ExtraServices
                }
            };
            return model;
        }
    }
}
