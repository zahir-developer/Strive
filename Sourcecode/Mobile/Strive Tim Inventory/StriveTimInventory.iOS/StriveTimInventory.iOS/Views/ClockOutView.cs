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
            ClockInView.Layer.CornerRadius = ClockOutViewBox.Layer.CornerRadius = 20;
            ClockInView.Layer.MaskedCorners = (CoreAnimation.CACornerMask)5;
            ClockOutViewBox.Layer.MaskedCorners = (CoreAnimation.CACornerMask)10;
            CreateBindings();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        private void CreateBindings()
        {
            var set = this.CreateBindingSet<ClockOutView, ClockOutViewModel>();
            set.Bind(LogoutButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(WelcomeBackLabel).To(vm => vm.WelcomeTitle);
            set.Bind(RoleLabel).To(vm => vm.Role);
            set.Bind(DateLabel).To(vm => vm.CurrentDate);
            set.Bind(ClockInTimeLabel).To(vm => vm.ClockInTime);
            set.Bind(ClockOutTimeLabel).To(vm => vm.ClockOutTime);
            set.Bind(TotalHoursLabel).To(vm => vm.TotalHours);
            set.Bind(Return).To(vm => vm.Commands["Logout"]);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

