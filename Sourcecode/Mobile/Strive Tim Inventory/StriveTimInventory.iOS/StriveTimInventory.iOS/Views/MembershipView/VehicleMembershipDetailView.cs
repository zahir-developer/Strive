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
            set.Bind(MembershipName).To(vm => vm.MembershipName);
            set.Bind(ActivatedDate).To(vm => vm.ActivatedDate);
            set.Bind(CancelledDate).To(vm => vm.CancelledDate);
            set.Bind(Status).To(vm => vm.Status);
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

