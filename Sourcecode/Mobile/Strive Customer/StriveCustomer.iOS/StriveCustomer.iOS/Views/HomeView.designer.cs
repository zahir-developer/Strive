// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    [Register ("HomeView")]
    partial class HomeView
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        MapKit.MKMapView WashTimeWebView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (WashTimeWebView != null) {
                WashTimeWebView.Dispose ();
                WashTimeWebView = null;
            }
        }
    }
}