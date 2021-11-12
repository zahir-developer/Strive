using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;
using Foundation;
using Strive.Core.Utils;
using WebKit;
using StriveCustomer.iOS.UIUtils;
using Strive.Core.Models.Employee.Documents;
using System.Threading.Tasks;
using Strive.Core.Models.Customer;

namespace StriveCustomer.iOS.Views.Login
{
    public partial class LoginView : MvxViewController<LoginViewModel>
    {
        
        NSUserDefaults Persistance;
        string UsernameKey = "username";
        string PasswordKey = "password";

        public bool FirstTimeFlag;
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
            //set.Bind(LoginButton).To(vm => vm.Commands["DoLogin"]);
            set.Bind(ForgotPasswordButton).To(vm => vm.Commands["ForgotPassword"]);
            set.Apply();
            TermsDocuments.Hidden = true;
            AgreeBtn.Hidden = true;
            DisagreeBtn.Hidden = true;
            
            SignupLbl.UserInteractionEnabled = true;
            
            //plist.SetBool(true, "first");
            Action action = () =>
            {
                UIApplication.SharedApplication.OpenUrl(new NSUrl(ApiUtils.URL_CUSTOMER_SIGNUP));
            };

            UITapGestureRecognizer tap = new UITapGestureRecognizer(action);
            SignupLbl.AddGestureRecognizer(tap);
            // Perform any additional setup after loading the view, typically from a nib.
        }
        partial  void LoginButtonClicked(UIButton sender)
        {
            CallLogin();
            
            

        }
        async void CallLogin()
        {
            await Task.Run(ViewModel.DoLogin);
            var plist = NSUserDefaults.StandardUserDefaults;
            var First = plist.BoolForKey("first");
            if (First == true)
            {
                SetTerm();
                TermsDocuments.Hidden = true;
                AgreeBtn.Hidden = true;
                DisagreeBtn.Hidden = true;
                ViewModel.navigatetodashboard();
            }
            else
            {

                ViewModel.navigatetodashboard();
            }
            
        }
        
        async void SetTerm()
        {
            TermsDocument term = await ViewModel.Terms();
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Terms and Conditions";
            
            WKWebView webView = new WKWebView(TermsDocuments.Bounds, new WKWebViewConfiguration());
            TermsDocuments.AddSubview(webView);
            LoadBase64StringToWebView(term.Document.Document.Base64, webView);

        }

        void LoadBase64StringToWebView(string base64String, WKWebView webview)
        {
            var data = new NSData(base64String, options: NSDataBase64DecodingOptions.IgnoreUnknownCharacters);
            webview.LoadData(data, mimeType: "application/pdf", characterEncodingName: "", baseUrl: NSUrl.FromString("https://www.google.com"));
        }

        partial void AgreeBtnclicked(UIButton sender)
        {
            TermsDocuments.Hidden = true;
            AgreeBtn.Hidden = true;
            DisagreeBtn.Hidden = true;
            ViewModel.navigatetodashboard();
            var plist = NSUserDefaults.StandardUserDefaults;
            var First = plist.BoolForKey("first");
            plist.SetBool(false, "first");

        }
        partial void DisagreeBtnclicked(UIButton sender)
        {
            TermsDocuments.Hidden = true;
            AgreeBtn.Hidden = true;
            DisagreeBtn.Hidden = true;

        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        void DoInitialSetup()
        {
            NavigationController.NavigationBarHidden = true;
            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
            Persistance = NSUserDefaults.StandardUserDefaults;
            SetCredentials();
        }

        partial void CheckBoxClicked(UIButton sender)
        {
            ViewModel.RememberMeButtonCommand();
            SetRememberMe();
        }

        void StoreCredentials()
        {
            
            Persistance.SetString(ViewModel.loginEmailPhone, UsernameKey);
            Persistance.SetString(ViewModel.loginPassword, PasswordKey);
        }

        void ClearCredentials()
        {
            var dictionary = Persistance.ToDictionary();
            foreach(var dict in dictionary)
            {
                Persistance.RemoveObject(dict.Key.ToString());
            }
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

        public override void ViewDidDisappear(bool animated)
        {
            if (ViewModel.rememberMe)
            {
                StoreCredentials();
            }
        }
    }
}

