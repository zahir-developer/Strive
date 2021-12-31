using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using StriveTimInventory.iOS.UIUtils;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class ClockedInView : MvxViewController<ClockedInViewModel>
    {
        public ClockedInView() : base("ClockedInView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CreateBindings();
            ClockinView.Layer.CornerRadius = ClockoutView.Layer.CornerRadius = 20;
            ClockinView.Layer.MaskedCorners = (CoreAnimation.CACornerMask)5;
            ClockoutView.Layer.MaskedCorners = (CoreAnimation.CACornerMask)10;
            ClockOutButton.Layer.CornerRadius = 3;
            NavigationController.NavigationBarHidden = true;
            // Perform any additional setup after loading the view, typically from a nib.
        }

        private void CreateBindings()
        {
            var set = this.CreateBindingSet<ClockedInView, ClockedInViewModel>();
            set.Bind(LogoutButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(ClockOutButton).To(vm => vm.Commands["NavigateClockOut"]);
            set.Bind(Return).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(WelcomeLabel).To(vm => vm.WelcomeTitle);
            set.Bind(RoleLabel).To(vm => vm.Role);
            set.Bind(DateLabel).To(vm => vm.CurrentDate);
            set.Bind(ClockInTimeLabel).To(vm => vm.ClockInTime);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

