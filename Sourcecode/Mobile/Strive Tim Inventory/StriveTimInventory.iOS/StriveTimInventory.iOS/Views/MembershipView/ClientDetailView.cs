using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;
using MvvmCross.Binding.BindingContext;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class ClientDetailView : MvxViewController<MembershipClientDetailViewModel>
    {
        public ClientDetailView() : base("ClientDetailView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<ClientDetailView, MembershipClientDetailViewModel>();
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

