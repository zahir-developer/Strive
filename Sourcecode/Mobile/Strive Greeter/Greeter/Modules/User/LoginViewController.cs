// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Modules.User;
using Greeter.Services.Network;
using MvvmCross.Binding.BindingContext;

namespace Greeter
{
    public partial class LoginViewController : BaseViewController
    {
        //bool isEyeOpen = false;

        public LoginViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Initialise();

            // Clicks
            btnLogin.TouchUpInside += delegate
            {
                _ = LoginClicked(tfUserId.Text, tfPswd.Text);
            };

            btnEye.TouchUpInside += delegate
            {
                ChaangeEye(tfPswd.SecureTextEntry);
            };

            DismissKeyboardOnTapArround = true;
        }

        void Initialise()
        {
            NavigationController.NavigationBar.Hidden = true;

            // Initial UI Customisation
            tfUserId.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfUserId.AddRightPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);

            tfPswd.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfPswd.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);

            //SetBinding();
        }

        void SetBinding()
        {
            // Set data to view
            //var set = this.CreateBindingSet<LoginViewController, LoginViewModel>();
            //set.Bind(tfUserId).To(vm => vm.Email);
            //set.Bind(btnLogin).To(vm => vm.LoginCmd);
            //set.Apply();
        }

        void ChaangeEye(bool isOpen)
        {
            if (isOpen)
            {
                // TODO : Close Eye Image Change and disable text shown in pswd

            }
            else
            {
                // TODO : Open Eye Image Change and show text in pswd
            }

            tfPswd.SecureTextEntry = !isOpen;
        }

        async Task LoginClicked(string email, string pswd)
        {
            // Validate Fields
            if (email.IsEmpty() && pswd.IsEmpty())
            {
                ShowAlertMsg(Common.Messages.USER_ID_AND_PSWD_EMPTY);
                return;
            }
            if (email.IsEmpty())
            {
                ShowAlertMsg(Common.Messages.USER_ID_EMPTY);
                return;
            }
            if (pswd.IsEmpty())
            {
                ShowAlertMsg(Common.Messages.PSWD_EMPTY);
                return;
            }

            // Do Login
            var req = new LoginRequest() { Email = email, Pswd = pswd };

            ShowActivityIndicator();
            var response = await new ApiService(new NetworkService()).DoLogin(req);
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                AppSettings.Token = response?.AuthToken;
                AppSettings.UserID = response?.EmployeeDetails?.EmployeeLogin?.EmployeeId ?? 0;
            }

            // Navigation
            NavigateToLocationScreen();
        }

        void NavigateToLocationScreen()
        {
            var vcLocation = this.Storyboard.InstantiateViewController(nameof(LocationViewController));

            NavigateToWithAnim(vcLocation);
        }
    }
}
