using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;
using Foundation;

using ObjCRuntime;
using WebKit;
using System.Collections.Generic;
using EventKit;
using System.Net.Http;
using System.Net.Http.Headers;
using Strive.Core.Utils.TimInventory;
using System.Text.RegularExpressions;
using System.Linq;

namespace StriveCustomer.iOS.Views
{
    public partial class GenbookView : MvxViewController<GenbookViewModel>
    {
        bool ShouldCloseWebView = false;

        List<string> urls= new List<string>();

        public GenbookView() : base("GenbookView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var set = this.CreateBindingSet<GenbookView, GenbookViewModel>();
            //set.Bind(LoginButton).To(vm => vm.Commands["DoLogin"]);
            set.Apply();
            EmployeeData.load = false;
            var url = new NSUrl("https://www.genbook.com/bookings/slot/reservation/31400780?bookingSourceId=1000");
            var request = new NSUrlRequest(url);
            GenbookWebView.Configuration.Preferences.JavaScriptEnabled = true;
            GenbookWebView.LoadRequest(request);
            GenbookWebView.NavigationDelegate = new WKWebViewDelegate();
            GenbookWebView.AddObserver(this, "URL", NSKeyValueObservingOptions.OldNew, IntPtr.Zero);
        }

        public override async void ObserveValue(NSString keyPath, NSObject ofObject, NSDictionary change, IntPtr context)
        {
            var url = change[ChangeNewKey];
            var urlstring = url.ToString();
            urls.Add(urlstring);
            if (urlstring.Equals("https://www.genbook.com/bookings/slot/reservation/31400780?bookingSourceId=1000"))
            {
                GenbookWebView.Configuration.Preferences.JavaScriptEnabled = true;
                GenbookWebView.LoadRequest(new NSUrlRequest(new NSUrl(urls[urls.Count - 2])));
                GenbookWebView.NavigationDelegate = new WKWebViewDelegate();
                EmployeeData.load = true;
                if (!ShouldCloseWebView)
                {
                    ShouldCloseWebView = true;
                    return;
                }
            }
        }

        public class WKWebViewDelegate : WKNavigationDelegate
        {
            public override async void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
            {
                webView.EvaluateJavaScript("document.getElementsByTagName('html')[0].innerHTML",(result, error) => {
                    
                    if(EmployeeData.load)
                    {
                        Console.WriteLine(result.ToString());
                        EmployeeData.ViewSourceUrl = result.ToString();

                        var chararray = EmployeeData.ViewSourceUrl.ToCharArray();

                        int index = 0;

                        string finalstring = "";

                        if (EmployeeData.ViewSourceUrl.Contains("Mon, "))
                        {
                            index = EmployeeData.ViewSourceUrl.IndexOf("Mon, ");
                        }
                        else if (EmployeeData.ViewSourceUrl.Contains("Tue, "))
                        {
                            index = EmployeeData.ViewSourceUrl.IndexOf("Tue, ");
                        }
                        else if (EmployeeData.ViewSourceUrl.Contains("Wed, "))
                        {
                            index = EmployeeData.ViewSourceUrl.IndexOf("Wed, ");
                        }
                        else if (EmployeeData.ViewSourceUrl.Contains("Thu, "))
                        {
                            index = EmployeeData.ViewSourceUrl.IndexOf("Thu, ");
                        }
                        else if (EmployeeData.ViewSourceUrl.Contains("Fri, "))
                        {
                            index = EmployeeData.ViewSourceUrl.IndexOf("Fri, ");
                        }
                        else if (EmployeeData.ViewSourceUrl.Contains("Sat, "))
                        {
                            index = EmployeeData.ViewSourceUrl.IndexOf("Sat, ");
                        }
                        else if (EmployeeData.ViewSourceUrl.Contains("Sun, "))
                        {
                            index = EmployeeData.ViewSourceUrl.IndexOf("Sun, ");
                        }

                        if(index > 0)
                        {
                            int ToIndex = index + 28;
                            for(int i = index; i< ToIndex; i++ )
                            {
                                finalstring += chararray[i].ToString();
                            }
                        }
                    }
                });

            }
            
            public override void DecidePolicy(WKWebView webView, WKNavigationAction navigationAction, Action<WKNavigationActionPolicy> decisionHandler)
            {
                var url1 = webView.Url;
                var url2 = navigationAction.Request;
                Console.WriteLine(url2);
                decisionHandler(WKNavigationActionPolicy.Allow);
            }

            public override void DecidePolicy(WKWebView webView, WKNavigationResponse navigationResponse, Action<WKNavigationResponsePolicy> decisionHandler)
            {
                var url1 = webView.Url;
                var url2 = navigationResponse.Response as NSHttpUrlResponse;
                Console.WriteLine(url2);

                decisionHandler(WKNavigationResponsePolicy.Allow);
            }

            public override void DidStartProvisionalNavigation(WKWebView webView, WKNavigation navigation)
            {
                var url = webView.Url;
            }
            public override void DidCommitNavigation(WKWebView webView, WKNavigation navigation)
            {
               
            }
            public override void DidReceiveServerRedirectForProvisionalNavigation(WKWebView webView, WKNavigation navigation)
            {
               
            }
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

