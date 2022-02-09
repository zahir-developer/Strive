using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;
using ZXing.Mobile;

namespace StriveCustomer.iOS.Views
{
    public partial class DealsPageViewController : MvxViewController<DealsPageViewModel>
    {

        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

        public List<string> couponCodes = new List<string>();
        private string EndDate;
        
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

            
            CouponLabel.Text = DealsViewModel.CouponName;
            if (DealsViewModel.SelectedDealId==1)
            {
                PriceView.Hidden = false;
                PriceStackView.Hidden = true;
            }
            else
            {
                PriceView.Hidden = true;
                PriceStackView.Hidden = false;
            }
            FirstPriceLabel.Text = "$0";
            SecondPriceLabel.Text = "$5";
            ThirdPriceLabel.Text = "$5";
            FourthPriceLabel.Text = "$10";
            FifthPriceLabel.Text = "$15";

            couponCodes.Add("ONEWASH");
            couponCodes.Add("BOUNCEBACK");

            ScanQrCodeButton.TouchUpInside += ScanQrCodeButton_TouchUpInsideAsync;
            //PriceView.Hidden = true;
            
            //PriceStackView.Hidden = true;
            ValidateDeals(true);
           

        }
        private void ValidateDeals(bool firstcall)
        {
            if (DealsPageViewModel.clientDeal != null)
            {
                if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 10 && DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealId == 1)
                {
                    if (!firstcall)
                    {
                        _userDialog.Alert("", DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealName + " Sucess");
                    }

                    FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    FourthProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    FifthProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    SixthProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    SeventhProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    EighthProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    NinthProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    TenthProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    PriceProgessBar.SetProgress(0.0f, false);
                    NoOfDaysLabel.Hidden = true;
                    DaysRemainingLabel.Hidden = true;


                }
                else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 5 && DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealId == 2)
                {
                    if (!firstcall)
                    {
                        _userDialog.Alert("You Save $15", DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealName + " Success");
                    }

                    FirstPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    SecondPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    ThirdPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    FourthPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    FifthPriceButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                    NoOfDaysLabel.Hidden = true;
                    DaysRemainingLabel.Hidden = true;
                }
                else
                {
                    SetProgress();
                    CouponValidity();
                }
            }
            else
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
        }
        public void SetProgress()
        {
            if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount==1)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);

                FirstPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.0f, false);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 2)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);

                FirstPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.15f, false);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 3)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);

                FirstPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.25f, false);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 4)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);

                FirstPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.35f, false);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 5)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FifthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);

                FirstPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FifthPriceButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.45f, false);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 6)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FifthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SixthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.55f, false);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 7)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FifthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SixthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SeventhProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.65f, false);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 8)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FifthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SixthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SeventhProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                EighthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.75f, false);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 9)
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FifthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SixthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SeventhProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                EighthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                NinthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.85f, false);

            }
            else 
            {
                FirstProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SecondProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                ThirdProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FourthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                FifthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SixthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                SeventhProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                EighthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                NinthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                TenthProgressBarButton.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                PriceProgessBar.SetProgress(0.95f, false);
            }

        }
        private void ScanQrCodeButton_TouchUpInsideAsync(object sender, EventArgs e)
        {
            var defaultTime = new TimeSpan(00, 00, 00);
            if (DealsPageViewModel.scannedTime > defaultTime)
            {
                ScanDelay();
            }
            else
            {
                startScanQrCode();
            }
        }      

        private void ScanDelay()
        {
            var timePeriod = DateTime.Now.TimeOfDay;            
            var timeInterval = timePeriod.Subtract(DealsPageViewModel.scannedTime);
            var span = new TimeSpan(0, 10, 0);
            if (timeInterval > span)
            {
                startScanQrCode();
            }
            else
            {                                
                showDialog("QR Scan", "Scan after a few minutes");
            }
        }

        private async void startScanQrCode()
        {
           
            string CurrentDate = DateTime.Today.ToString("yyyy-MM-dd");
            if (DealsViewModel.TimePeriod == 3)
            {
                if ((DateTime.Today.Date <= DateTime.Parse(DealsViewModel.enddate).Date))
                {
                    var scanner = new MobileBarcodeScanner();

                    var result = await scanner.Scan();

                    if (result != null)
                    {
                        Console.WriteLine("Scanned Barcode: " + result.Text); //ONEWASH
                        if (couponCodes[DealsViewModel.SelectedDealId - 1].ToUpper().Replace(" ", "") == result.Text.ToUpper().Replace(" ", ""))
                        {
                            ViewModel.dealId = DealsViewModel.SelectedDealId;
                            ViewModel.clientID = CustomerInfo.ClientID;
                            ViewModel.Date = DateTime.Today.ToString("yyyy-MM-dd");
                            //_userDialog.Loading();

                            await ViewModel.AddClientDeals();
                            DealsPageViewModel.scannedTime = DateTime.Now.TimeOfDay;
                            ValidateDeals(false);


                            //_userDialog.HideLoading();
                        }
                        else
                        {
                            _userDialog.Alert("The Scanned Coupon not matched!,Try Again", "Invalid Coupon");
                        }
                    }
                    else
                    {
                        _userDialog.Alert("The Scanned Coupon not matched!,Try Again", "Invalid Coupon");
                    }
                }
                else
                {
                    _userDialog.Alert("Deal not available");
                }

            }
            else
            {
                var scanner = new MobileBarcodeScanner();

                var result = await scanner.Scan();
                if (result != null)
                {
                    Console.WriteLine("Scanned Barcode: " + result.Text); //ONEWASH
                    if (couponCodes[DealsViewModel.SelectedDealId - 1].ToUpper().Replace(" ", "") == result.Text.ToUpper().Replace(" ", ""))
                    {
                        ViewModel.dealId = DealsViewModel.SelectedDealId;
                        ViewModel.clientID = CustomerInfo.ClientID;
                        ViewModel.Date = DateTime.Today.ToString("yyyy-MM-dd");
                        //_userDialog.Loading();

                        await ViewModel.AddClientDeals();
                        DealsPageViewModel.scannedTime = DateTime.Now.TimeOfDay;
                        ValidateDeals(false);


                        //_userDialog.HideLoading();
                    }
                    else
                    {
                        _userDialog.Alert("The Scanned Coupon not matched!,Try Again", "Invalid Coupon");
                    }
                }
                else
                {
                    _userDialog.Alert("The Scanned Coupon not matched!,Try Again", "Invalid Coupon");
                }
            }
            //if (result.Text == "Buy 10, Get next FREE")
            //{
            //    ViewModel.AddClientDeals();
            //}
        }



        private void showDialog(string Title, string Message)
        {
            var okAlertController = UIAlertController.Create(Title, Message, UIAlertControllerStyle.Alert);            
            okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));            
            PresentViewController(okAlertController, true, null);
        }

        private void CouponValidity()
        {

            if (DealsPageViewModel.clientDeal != null)
            {
                NoOfDaysLabel.Hidden = false;
                DaysRemainingLabel.Hidden = false;
                DateTime startdate = DateTime.Today;
                DateTime enddate = DateTime.Parse(DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].EndDate);
                NoOfDaysLabel.Text = (enddate.Date - startdate.Date).ToString("dd");
            }
            else
            {
                NoOfDaysLabel.Hidden = true;
                DaysRemainingLabel.Hidden = true;
            }

        }

    }
}

