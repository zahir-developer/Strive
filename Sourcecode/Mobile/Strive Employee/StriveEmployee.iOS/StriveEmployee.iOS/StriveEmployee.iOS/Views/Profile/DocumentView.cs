using System;
using Foundation;
using Strive.Core.Utils.Employee;
using StriveEmployee.iOS.UIUtils;
using UIKit;
using WebKit;

namespace StriveEmployee.iOS.Views.Profile
{
    public partial class DocumentView : UIViewController
    {
        public DocumentView() : base("DocumentView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetView();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public void SetView()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Document";

            WKWebView webView = new WKWebView(View.Bounds, new WKWebViewConfiguration());
            View.AddSubview(webView);

            LoadBase64StringToWebView(MyProfileTempData.DocumentString, webView);
        }

        void LoadBase64StringToWebView(string base64String, WKWebView webview)
        {
            var data = new NSData(base64String, options: NSDataBase64DecodingOptions.IgnoreUnknownCharacters);
            webview.LoadData(data, mimeType: "application/pdf", characterEncodingName: "", baseUrl: NSUrl.FromString("https://www.google.com"));
        }
    }
}

