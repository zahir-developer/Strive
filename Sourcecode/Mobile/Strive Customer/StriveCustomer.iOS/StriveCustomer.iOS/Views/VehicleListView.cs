using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;
using Foundation;

namespace StriveCustomer.iOS.Views
{
    public partial class VehicleListView : MvxViewController<VehiclelistViewModel>
    {
        public VehicleListView() : base("VehicleListView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var set = this.CreateBindingSet<VehicleListView, VehiclelistViewModel>();
            //set.Bind(LoginButton).To(vm => vm.Commands["DoLogin"]);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void VehicleTapped(UIButton sender)
        {
            ViewModel.NavigateToGenbook(sender.Title(UIControlState.Normal));
        }
    }
}

