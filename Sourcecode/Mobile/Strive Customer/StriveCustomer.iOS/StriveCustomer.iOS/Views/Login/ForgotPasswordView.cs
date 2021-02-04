﻿using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views.Login
{
    public partial class ForgotPasswordView : MvxViewController<ForgotPasswordViewModel>
    {
        public ForgotPasswordView() : base("ForgotPasswordView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            var set = this.CreateBindingSet<ForgotPasswordView, ForgotPasswordViewModel>();
            set.Bind(GetOtpButton).To(vm => vm.Commands["GetOTP"]);
            set.Bind(BackButton).To(vm => vm.Commands["GetOTP"]);
            set.Bind(MobileTextfield).To(vm => vm.resetEmail);
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

        partial void BackBtn_Touch(UIButton sender)
        {
            this.ViewModel.NavigateBackCommand();
        }
    }
}

