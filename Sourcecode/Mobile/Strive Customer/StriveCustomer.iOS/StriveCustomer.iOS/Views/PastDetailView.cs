using System;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class PastDetailView : MvxViewController//<LoginViewModel>
    {
        public PastDetailView() : base("PastDetailView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;

            //var PastDetailTableSource = new PastDetailTableSource(PastDetailTableView, ViewModel);

            //PastDetailTableView.Source = PastDetailTableSource;
            //PastDetailTableView.TableFooterView = new UIView(CGRect.Empty);
            //PastDetailTableView.DelaysContentTouches = false;
            //PastDetailTableView.ReloadData();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

