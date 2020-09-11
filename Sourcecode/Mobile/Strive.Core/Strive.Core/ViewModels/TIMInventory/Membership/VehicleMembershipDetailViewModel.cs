using System;
using System.Linq;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class VehicleMembershipDetailViewModel : BaseViewModel
    {
        public VehicleMembershipDetailViewModel()
        {
            GetDetails();
        }


        void GetDetails()
        {
            MembershipName = MembershipData.MembershipServiceList.Membership.Where(m => m.MembershipId == MembershipDetail.MembershipId).Select(m => m.MembershipName).FirstOrDefault();
        }

        public ClientVehicleMembershipView MembershipDetail { get; set; } = MembershipData.MembershipDetailView;

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
    }
}
