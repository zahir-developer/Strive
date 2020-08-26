using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory.Membership;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class VehicleMembershipDetailView : MvxViewController<VehicleMembershipDetailViewModel>
    {
        public VehicleMembershipDetailView() : base("VehicleMembershipDetailView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<VehicleMembershipDetailView, VehicleMembershipDetailViewModel>();
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

