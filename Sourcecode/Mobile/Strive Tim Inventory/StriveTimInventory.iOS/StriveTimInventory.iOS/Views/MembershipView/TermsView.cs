using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class TermsView : MvxViewController<TermsViewModel>
    {
        public TermsView() : base("TermsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<TermsView, TermsViewModel>();
            set.Bind(AgreeButton).To(vm => vm.Commands["Next"]);
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(DisagreeButton).To(vm => vm.Commands["Disagree"]);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

