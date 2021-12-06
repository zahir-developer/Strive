using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using AVFoundation;
using CoreGraphics;
using Foundation;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using UIKit;
using ZXing.Mobile;

namespace StriveCustomer.iOS.Views
{
    public partial class DealsView : MvxViewController<DealsViewModel>
    {
        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();
        //DealsPageViewModel DealsPageViewModel = new DealsPageViewModel();
        public DealsView() : base("DealsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };

            NavigationItem.Title = "Deals";

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };


            var DealsTableSource = new DealsTableSource(VehiclesTableView, ViewModel);

            var set = this.CreateBindingSet<DealsView, DealsViewModel>();
            set.Bind(DealsTableSource).To(vm => vm.Deals);
            //set.Bind(DealsTableSource).For(s => s.SelectionChangedCommand).To(vm => vm.Commands["NavigateToDetail"]);
            set.Apply();

            VehiclesTableView.Source = DealsTableSource;
            VehiclesTableView.TableFooterView = new UIView(CGRect.Empty);
            VehiclesTableView.DelaysContentTouches = false;
            VehiclesTableView.ReloadData();

            ScanButton.TouchUpInside += ScanButton_TouchUpInside;
        }

        private async void ScanButton_TouchUpInside(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                Console.WriteLine("Scanned Barcode: " + result.Text);

                _userDialog.Alert("Your coupon code is : " + result.Text);

            }
           
            
        }

        public override  void ViewDidAppear(bool animated)
        {
             getDeals();
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public async Task getDeals()
        {
            await ViewModel.GetAllDealsCommand();
            IsCameraAuthorized();
        }

        public bool IsCameraAuthorized()
        {
            AVAuthorizationStatus authStatus = AVCaptureDevice.GetAuthorizationStatus(AVMediaType.Video);

            switch (authStatus)
            {
                case AVAuthorizationStatus.NotDetermined:
                    return false;                   
                case AVAuthorizationStatus.Restricted:
                    return false;
                case AVAuthorizationStatus.Denied:
                    return false;
                case AVAuthorizationStatus.Authorized:
                    return true;
                default:
                   return false;
            }
        }
    }
}

