using System;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using ObjCRuntime;
using TipCalc.Core.ViewModels;
using UIKit;
using WebKit;

namespace TipCalc.iOS.Views
{
    public partial class Tab1View : MvxViewController<Tab1ViewModel>
    {
        public Tab1View() : base("Tab1View", null)
        {
        }
        bool ShouldCloseWebView = false;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            var url = new NSUrl("https://telliant2.genbook.com/?bookingSourceId=1");
            var request = new NSUrlRequest(url);
            webView.LoadRequest(request);
            webView.NavigationDelegate = new WKWebViewDelegate();
            webView.AddObserver(this, "URL", NSKeyValueObservingOptions.OldNew, IntPtr.Zero);
        }

        public override async void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
        {
            var url = change[ChangeNewKey];
            var urlstring = url.ToString();
            if (urlstring.Equals("https://www.genbook.com/bookings/slot/reservation/31318540?bookingSourceId=1"))
            {
                if(!ShouldCloseWebView)
                {
                    ShouldCloseWebView = true;
                    return;
                }
                await ViewModel.NavigateBackCommand();
            }
        }

        public class WKWebViewDelegate : WKNavigationDelegate
        {
            public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
            {
                
            }
            public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
            {
                var url1 = webView.Url;
                var url2 = navigationAction.Request.Url?.AbsoluteString;
                decisionHandler(WKNavigationActionPolicy.Allow);
            }

            public override void DecidePolicy(WKWebView webView, WKNavigationResponse navigationResponse, Action<WKNavigationResponsePolicy> decisionHandler)
            {
                var url1 = webView.Url;
                var url2 = navigationResponse.Response.Url?.AbsoluteString;
                decisionHandler(WKNavigationResponsePolicy.Allow);
            }

            public override void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
            {
                var url = webView.Url;
            }

         

        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

