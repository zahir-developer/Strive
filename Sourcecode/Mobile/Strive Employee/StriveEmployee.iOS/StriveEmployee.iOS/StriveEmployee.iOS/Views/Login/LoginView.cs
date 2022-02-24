﻿using System;
using Firebase.CloudMessaging;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Utils;
using Strive.Core.ViewModels.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views.Login
{
    public partial class LoginView :MvxViewController<LoginViewModel>
    {
        NSUserDefaults Persistance;
        string UsernameKey = "username";
        string PasswordKey = "password";
        string TermsKey = "false";

        public LoginView() : base("LoginView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
            var set = this.CreateBindingSet<LoginView, LoginViewModel>();
            set.Bind(EmailTextfield).To(vm => vm.loginEmailPhone);
            set.Bind(PasswordTextfield).To(vm => vm.loginPassword);
            //set.Bind(LoginBtn).To(vm => vm.Commands["DoLogin"]);
            set.Bind(ForgotPasswordBtn).To(vm => vm.Commands["ForgotPassword"]);
            set.Apply();
            TermsAndCondtions.Hidden = true;
            TermsAndCondtions.BecomeFirstResponder();
            TermsAndCondtions.Layer.CornerRadius = 5;
            AgreeBtn.Layer.CornerRadius = 3;
            DisagreeBtn.Layer.CornerRadius = 3;

            SignUPLbl.UserInteractionEnabled = true;
            Action action = () =>
            {
                UIApplication.SharedApplication.OpenUrl(new NSUrl(ApiUtils.URL_CUSTOMER_SIGNUP));
            };
            UITapGestureRecognizer tap = new UITapGestureRecognizer(action);
            SignUPLbl.AddGestureRecognizer(tap);

            
            // Perform any additional setup after loading the view, typically from a nib.
        }

        void DoInitialSetup()
        {
            NavigationController.NavigationBarHidden = true;
            LoginBtn.Layer.CornerRadius = 5;
            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
            Persistance = NSUserDefaults.StandardUserDefaults;
            SetCredentials();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        partial void AgreeBtnClicked(UIButton sender)
        {
            bool terms = Persistance.BoolForKey(TermsKey);
            ViewModel.terms = !terms;
            Persistance.SetBool(ViewModel.terms, TermsKey);

            ViewModel.DoLoginCommand();
            TermsAndCondtions.Hidden = true;
        }

        partial void DisagreeBtnClicked(UIButton sender)
        {
            TermsAndCondtions.Hidden = true;
        }

        partial void LoginBtnClicked(UIButton sender)
        {
            ViewModel.token = Messaging.SharedInstance.FcmToken;

            if (Persistance.BoolForKey(TermsKey) == true)
            {
                ViewModel.DoLoginCommand();
            }
            else
            {
                TermsAndCondtions.Hidden = false;
            }

        }
        partial void CheckBoxClicked(UIButton sender)
        {
            ViewModel.RememberMeButtonCommand();
            SetRememberMe();
        }
        void SetCredentials()
        {
            var username = Persistance.StringForKey(UsernameKey);
            var password = Persistance.StringForKey(PasswordKey);
            if (username != null && password != null)
            {
                ViewModel.loginEmailPhone = username;
                ViewModel.loginPassword = password;
                ViewModel.rememberMe = true;
                SetRememberMe();
            }
            
        }

        void SetRememberMe()
        {
            if (ViewModel.rememberMe)
            {
                CheckBox.SetImage(UIImage.FromBundle("icon-checked"), UIControlState.Normal);
                StoreCredentials();
                return;
            }
            ClearCredentials();
            CheckBox.SetImage(UIImage.FromBundle("icon-unchecked"), UIControlState.Normal);
        }

        void ClearCredentials()
        {
            var dictionary = Persistance.ToDictionary();
            foreach (var dict in dictionary)
            {
                Persistance.RemoveObject(dict.Key.ToString());
            }
        }

        void StoreCredentials()
        {
            Persistance.SetString(ViewModel.loginEmailPhone, UsernameKey);
            Persistance.SetString(ViewModel.loginPassword, PasswordKey);
        }

        public override void ViewDidDisappear(bool animated)
        {
            if (ViewModel.rememberMe)
            {
                StoreCredentials();
            }
        }
    }
}

