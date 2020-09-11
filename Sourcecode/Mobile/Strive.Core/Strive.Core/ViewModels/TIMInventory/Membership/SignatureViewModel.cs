using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using System.Collections.Generic;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SignatureViewModel : BaseViewModel
    {
        public SignatureViewModel()
        {
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public async void NextCommand()
        {
            var VehicleMembership = PrepareVehicleMembership();
            var result = await AdminService.SaveVehicleMembership(VehicleMembership);
            if(result == null)
            {
                await _userDialog.AlertAsync("Membership not Assigned");
                return;
            }
            if(result.Status)
            {
                await _userDialog.AlertAsync("Membership Assigned");
                _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 5, "exit!"));
            }
            else
            {
                await _userDialog.AlertAsync("Membership not Assigned");
            }
        }

        ClientVehicleRoot PrepareVehicleMembership()
        {
            return new ClientVehicleRoot()
            {
                clientVehicle = new ClientVehicle() { clientVehicle = null },
                clientVehicleMembershipModel = new ClientVehicleMembershipModel()
                {
                    clientVehicleMembershipDetails = new ClientVehicleMembershipDetails()
                    {
                        clientMembershipId = 0,
                        clientVehicleId = MembershipData.SelectedVehicle.VehicleId,
                        locationId = 1,
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
        }
    }
}
