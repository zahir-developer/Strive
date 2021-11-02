using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.Login
{
    public partial class NewPasswordView : MvxViewController<ConfirmPasswordViewModel>
    {
        public NewPasswordView() : base("NewPasswordView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            var set = this.CreateBindingSet<NewPasswordView, ConfirmPasswordViewModel>();
            set.Bind(NewPasswordTextField).To(vm => vm.NewPassword);
            set.Bind(ConfirmPasswordTextField).To(vm => vm.ConfirmPassword);
            set.Bind(submitPassword).To(vm => vm.Commands["Submit"]);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void DoInitialSetup()
        {
            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
        }
    }
}

