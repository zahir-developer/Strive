using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;
using ZXing.Mobile;

namespace StriveCustomer.iOS.Views
{
    public partial class DealsPageViewController : MvxViewController<DealsPageViewModel>
    {

        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

        public DealsPageViewController() : base("DealsPageViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        void InitialSetup()
        {
            
            DealsLabel.Layer.CornerRadius = 30;
            DealsLabel.ClipsToBounds = true;

            NoOfDaysLabel.Text = "30";

            BounceBackCouponLabel.Layer.CornerRadius = 10;

            FirstPriceLabel.Text = "$0";
            SecondPriceLabel.Text = "$5";
            ThirdPriceLabel.Text = "$5";
            FourthPriceLabel.Text = "$10";
            FifthPriceLabel.Text = "$15";

            FirstPriceButton.TouchUpInside += FirstPriceButton_TouchUpInside;
            SecondPriceButton.TouchUpInside += SecondPriceButton_TouchUpInside;
            ThirdPriceButton.TouchUpInside += ThirdPriceButton_TouchUpInside;
            FourthPriceButton.TouchUpInside += FourthPriceButton_TouchUpInside;
            FifthPriceButton.TouchUpInside += FifthPriceButton_TouchUpInside;

            ScanQrCodeButton.TouchUpInside += ScanQrCodeButton_TouchUpInsideAsync;

            //PriceView.Hidden = true;
            PriceStackView.Hidden = true;
            if (PriceProgessBar.Progress == 1)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
            
        }

        private async void ScanQrCodeButton_TouchUpInsideAsync(object sender, EventArgs e)
        {
            var scanner = new MobileBarcodeScanner();

            var result = await scanner.Scan();

            if (result != null)
            {
                Console.WriteLine("Scanned Barcode: " + result.Text);

                _userDialog.Alert("Your coupon code is : " + result.Text);

            }
        }
                
        private void FifthPriceButton_TouchUpInside(object sender, EventArgs e)
        {
            if (FifthPriceButton.CurrentImage == UIImage.FromBundle("icon-checked-round"))
            {
                FifthPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
            else
            {
                FifthPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            }
        }

        private void FourthPriceButton_TouchUpInside(object sender, EventArgs e)
        {
            if (FourthPriceButton.CurrentImage == UIImage.FromBundle("icon-checked-round"))
            {
                FourthPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
            else
            {
                FourthPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            }
        }

        private void ThirdPriceButton_TouchUpInside(object sender, EventArgs e)
        {
            if (ThirdPriceButton.CurrentImage == UIImage.FromBundle("icon-checked-round"))
            {
                ThirdPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
            else
            {
                ThirdPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            }
        }

        private void SecondPriceButton_TouchUpInside(object sender, EventArgs e)
        {
            if (SecondPriceButton.CurrentImage == UIImage.FromBundle("icon-checked-round"))
            {
                SecondPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
            else
            {
                SecondPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            }
        }

        private void FirstPriceButton_TouchUpInside(object sender, EventArgs e)
        {
            if (FirstPriceButton.CurrentImage == UIImage.FromBundle("icon-checked-round"))
            {
                FirstPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
            else
            {
                FirstPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
            }
            
        }
       
    }
}

