using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using StriveTimInventory.iOS.UIUtils;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class ClockOutView : MvxViewController<ClockOutViewModel>
    {
        public ClockOutView() : base("ClockOutView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CreateBindings();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        private void CreateBindings()
        {
            var set = this.CreateBindingSet<ClockOutView, ClockOutViewModel>();
            set.Bind(LogoutButton).To(vm => vm.Commands["NavigateBack"]);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

