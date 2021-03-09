using System;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views.Login
{
    public partial class LoginView :MvxViewController<LoginViewModel>
    {
        NSUserDefaults Persistance;
        string UsernameKey = "username";
        string PasswordKey = "password";

        public LoginView() : base("LoginView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();      
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

