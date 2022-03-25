using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
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
            JpegDocumentView.Hidden = true;
            DocumentWebView.Layer.CornerRadius = 10;
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
            ;
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                //ForegroundColor = UIColor.Clear.FromHex(0x24489A),
                //BackgroundColor = UIColor.Clear
            };
            NavigationItem.Title = "Document";
            //new CGRect(0,20,View.Frame.Width, View.Frame.Height+20)
            WKWebView webView = new WKWebView(new CGRect(5, 5, DocumentWebView.Frame.Width-22, DocumentWebView.Frame.Height-190), new WKWebViewConfiguration());
            //webView.ScrollView.Bounces = false;
            
            DocumentWebView.AddSubview(webView);
            LoadBase64StringToWebView(MyProfileTempData.DocumentString, webView);
        }

        void LoadBase64StringToWebView(string base64String, WKWebView webview)
        {
            if (DocumentsViewModel.IsImage)
            {
                webview.Hidden = true;
                JpegDocumentView.Hidden = false;
                byte[] encodedDataAsBytes = System.Convert.FromBase64String(base64String);
                NSData ImageData = NSData.FromArray(encodedDataAsBytes);
                var img = UIImage.LoadFromData(ImageData);
                JpegDocumentView.Image = img;
            }
            else
            {
                JpegDocumentView.Hidden = true;
                var data = new NSData(base64String, options: NSDataBase64DecodingOptions.IgnoreUnknownCharacters);
                webview.LoadData(data, mimeType: "application/pdf", characterEncodingName: "", baseUrl: NSUrl.FromString("https://www.google.com"));
            }
        }
    }
}

