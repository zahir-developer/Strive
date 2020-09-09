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
                        clientVehicleId = 1,
                        locationId = 1,
                        membershipId = MembershipData.SelectedMembership.MembershipId,
                        startDate = DateUtils.GetTodayDateString(),
                        endDate = DateUtils.GetTodayDateString(),
                        status = true,
                        notes = "",
                        isActive = true,
                        isDeleted = false
                    },
                    clientVehicleMembershipService = new List<ClientVehicleMembershipService>()
                    {
                        new ClientVehicleMembershipService()
                        {
                            clientMembershipId = 0,
                            serviceId = 2135,
                            serviceTypeId = 0,
                            isActive = true,
                            isDeleted = false,
                        }
                    }
                }
            };
        }
    }
}
