using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class VehicleMembershipDetailViewModel : BaseViewModel
    {
        public VehicleMembershipDetailViewModel()
        {
           
        }

        public ClientVehicleMembershipView MembershipDetail { get; set; } = MembershipData.MembershipDetail;

        public string MembershipName { get { return MembershipDetail.MembershipName; } set { } }

        public string ActivatedDate { get { return MembershipDetail.StartDate.ToShortDateString(); } set { } }

        public string CancelledDate { get { return MembershipDetail.EndDate.ToShortDateString(); } set { } }

        public string Status { get { return MembershipDetail.Status ? "Active" : "Inactive"; } set { } }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }
    }
}
