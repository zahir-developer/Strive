using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleMembershipDetailsViewModel : BaseViewModel
    {
        #region Properties

        public string MembershipName { get; set; }
        #endregion Properties



        #region Commands

        public async Task GetMembershipInfo()
        {
            _userDialog.ShowLoading();
           
            var data = await AdminService.GetMembershipServiceList();

            MembershipName = data.Membership.
                            Find(x=> x.MembershipId == CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.MembershipId).   
                            MembershipName;

            _userDialog.HideLoading();
        }

        #endregion Commands


    }
}
