using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.Utils;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class MembershipDetailView : MvxViewController<VehicleMembershipDetailsViewModel>
    {
        public MembershipDetailView() : base("MembershipDetailView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void InitialSetup()
        {
            MembershipDetail_ParentView.Layer.CornerRadius = 5;
            CancelMembership_Btn.Layer.CornerRadius = 5;

            GetMembershipInfo();
        }

        private async void GetMembershipInfo()
        {
            await this.ViewModel.GetMembershipInfo();
            if (!string.IsNullOrEmpty(this.ViewModel.MembershipName))
            {
                MembershipNameLbl.Text = this.ViewModel.MembershipName;
            }

            var CreatedDate = DateUtils.ConvertDateTimeFromZ(CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.StartDate.ToString());
            var DeletedDate = DateUtils.ConvertDateTimeFromZ(CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.EndDate.ToString());

            ActivatedDate_Value.Text = CreatedDate;
            CancelledDate_Value.Text = DeletedDate;
            if (!CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails.ClientVehicleMembership.IsActive.HasValue)
            {
                MembershipStatus_Value.Text = "Active";
            }
            else
            {
                MembershipStatus_Value.Text = "InActive";
            }
        }

        partial void CancelMembership_BtnTouch(UIButton sender)
        {
            CancelMembership();
        }

        private async void CancelMembership()
        {
            await this.ViewModel.CancelMembership();
            ViewModel.NavToProfileView();
        }
    }
}