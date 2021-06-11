using System;
using System.Threading.Tasks;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.UIUtils;
using UIKit;
using ZXing.Mobile;

namespace StriveCustomer.iOS.Views
{
    public partial class DealsView : MvxViewController<DealsViewModel>
    {
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

            //LogoutButton.Title = "Logout";
            //LogoutButton.SetTitleTextAttributes(new UITextAttributes()
            //{
            //    Font = DesignUtils.OpenSansRegularTwelve(),
            //    TextColor = UIColor.Clear.FromHex(0x24489A),
            //}, UIControlState.Normal);
            //NavigationItem.LeftBarButtonItem = LogoutButton;


            var DealsTableSource = new DealsTableSource(VehiclesTableView, ViewModel);

            var set = this.CreateBindingSet<DealsView, DealsViewModel>();
            set.Bind(DealsTableSource).To(vm => vm.DealsList);
            set.Bind(DealsTableSource).For(s => s.SelectionChangedCommand).To(vm => vm.Commands["NavigateToDetail"]);
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
                Console.WriteLine("Scanned Barcode: " + result.Text);
        }

        public override async void ViewDidAppear(bool animated)
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
        }
    }
}

