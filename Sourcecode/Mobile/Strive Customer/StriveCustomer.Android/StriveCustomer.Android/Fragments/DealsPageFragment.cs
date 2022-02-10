using MvvmCross.Droid.Support.V4;
using Android.OS;
using Android.Views;
using Strive.Core.ViewModels.Customer;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Android.Widget;
using Android.Support.V7.App;
using System;
using Strive.Core.Models.Customer;
using ZXing.Mobile;
using System.Collections.Generic;
using Android.Support.V4.App;
using Android;
using Android.Content.PM;
using StriveCustomer.Android.Services;
using Android.Graphics;
using Android.Content;
using OperationCanceledException = System.OperationCanceledException;
using System.Threading.Tasks;

namespace StriveCustomer.Android.Fragments
{
    public class DealsPageFragment : MvxFragment<DealsPageViewModel>
    {
        private Button backButton, dealsnameBtn,qrCodeScan;
        private DealsFragment dealsFragment;
        private TextView NoOfDaysTxtView, DaysRemainingTxtView;
        private CheckBox checkCoupon1,checkCoupon2, checkCoupon3, checkCoupon4, checkCoupon5, checkCoupon6, checkCoupon7, checkCoupon8, checkCoupon9, checkCoupon10;
        private CheckBox checkPrice1, checkPrice2, checkPrice3, checkPrice4, checkPrice5;
        private LinearLayout bounce_linearLayout, buy10_linearLayout;
        private List<string> couponCodes = new List<string>();      
       
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.DealsPageFragment, null);
            backButton = rootview.FindViewById<Button>(Resource.Id.dealsBack);
            NoOfDaysTxtView = rootview.FindViewById<TextView>(Resource.Id.NoOfDaysTxt);
            this.ViewModel = new DealsPageViewModel();
            DaysRemainingTxtView = rootview.FindViewById<TextView>(Resource.Id.daysRemainTxt);
            checkCoupon1 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon1);
            checkCoupon2 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon2);
            checkCoupon3 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon3);
            checkCoupon4 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon4);
            checkCoupon5 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon5);
            checkCoupon6 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon6);
            checkCoupon7 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon7);
            checkCoupon8 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon8);
            checkCoupon9 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon9);
            checkCoupon10 = rootview.FindViewById<CheckBox>(Resource.Id.checkCoupon10);

            checkPrice1 = rootview.FindViewById<CheckBox>(Resource.Id.checkPrice1);
            checkPrice2 = rootview.FindViewById<CheckBox>(Resource.Id.checkPrice2);
            checkPrice3 = rootview.FindViewById<CheckBox>(Resource.Id.checkPrice3);
            checkPrice4 = rootview.FindViewById<CheckBox>(Resource.Id.checkPrice4);
            checkPrice5 = rootview.FindViewById<CheckBox>(Resource.Id.checkPrice5);
            bounce_linearLayout = rootview.FindViewById<LinearLayout>(Resource.Id.bounce_couponLayout);
            buy10_linearLayout = rootview.FindViewById<LinearLayout>(Resource.Id.buy10_Layout);
            dealsnameBtn = rootview.FindViewById<Button>(Resource.Id.btnCoupon);
            qrCodeScan = rootview.FindViewById<Button>(Resource.Id.qrCodeScan);

            checkCoupon1.Enabled = false;
            checkCoupon2.Enabled = false;
            checkCoupon3.Enabled = false;
            checkCoupon4.Enabled = false;
            checkCoupon5.Enabled = false;
            checkCoupon6.Enabled = false;
            checkCoupon7.Enabled = false;
            checkCoupon8.Enabled = false;
            checkCoupon9.Enabled = false;
            checkCoupon10.Enabled = false;

            checkPrice1.Enabled = false;
            checkPrice2.Enabled = false;
            checkPrice3.Enabled = false;
            checkPrice4.Enabled = false;
            checkPrice5.Enabled = false;
            dealsFragment = new DealsFragment();
            dealsnameBtn.Text = DealsViewModel.CouponName;
            if (DealsViewModel.SelectedDealId == 1)
            {
                bounce_linearLayout.Visibility = ViewStates.Visible;
                buy10_linearLayout.Visibility = ViewStates.Gone;
            }
            else
            {
                bounce_linearLayout.Visibility = ViewStates.Gone;
                buy10_linearLayout.Visibility = ViewStates.Visible;
            }
            checkPrice1.Text = "$0";
            checkPrice2.Text = "$5";
            checkPrice3.Text = "$5";
            checkPrice4.Text = "$10";
            checkPrice5.Text = "$15";

            couponCodes.Add("ONEWASH");
            couponCodes.Add("BOUNCEBACK");
            backButton.Click += backButton_Click;
            qrCodeScan.Click += scanQrCodeButton_Async;
            couponValidity();
            validateDeals(true);
            return rootview;
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, dealsFragment).Commit();
        }
        private void couponValidity()
        {

            if (DealsPageViewModel.clientDeal != null)
            {
                NoOfDaysTxtView.Visibility = ViewStates.Visible;
                DaysRemainingTxtView.Visibility = ViewStates.Visible;
                DateTime startdate = DateTime.Today;
                DateTime enddate = DateTime.Parse(DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].EndDate);
                NoOfDaysTxtView.Text = (enddate.Date - startdate.Date).ToString("dd");
            }
            else
            {
                NoOfDaysTxtView.Visibility = ViewStates.Invisible;
                DaysRemainingTxtView.Visibility = ViewStates.Invisible;
            }

        }
        private void validateDeals(bool firstcall)
        {
            
            if (DealsPageViewModel.clientDeal != null)
            {
                if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 10 && DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealId == 1)
                {
                    if (!firstcall)
                    {
                        //UserDialogs.Instance.Alert("", DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealName + " Sucess");
                        showDialog("", DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealName , " Sucess");

                    }

                    checkCoupon1.Checked = false;
                    checkCoupon2.Checked = false;
                    checkCoupon3.Checked = false;
                    checkCoupon4.Checked = false;
                    checkCoupon5.Checked = false;
                    checkCoupon6.Checked = false;
                    checkCoupon7.Checked = false;
                    checkCoupon8.Checked = false;
                    checkCoupon9.Checked = false;
                    checkCoupon10.Checked = false;
                }
                else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 5 && DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealId == 2)
                {
                    if (!firstcall)
                    {
                        //UserDialogs.Instance.Alert("You Save $15", DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealName + " Success");
                        showDialog("You Save $15", DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealName, "Sucess");

                    }

                    checkPrice1.Checked = false;
                    checkPrice2.Checked = false;
                    checkPrice3.Checked = false;
                    checkPrice4.Checked = false;
                    checkPrice5.Checked = false;
                }
                else
                {
                    setProgress();
                }
            }
            else
            {
                checkCoupon1.Checked = false;
            }
        }
        public void setProgress()
        {
            if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 1)
            {
                checkCoupon1.Checked = true;
                checkPrice1.Checked = true;
               
                bounce_linearLayout.Background.SetLevel(0);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 2)
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkPrice1.Checked = true;
                checkPrice2.Checked = true;
                bounce_linearLayout.Background.SetLevel(1500);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 3)
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkCoupon3.Checked = true;
                checkPrice1.Checked = true;
                checkPrice2.Checked = true;
                checkPrice3.Checked = true;
                bounce_linearLayout.Background.SetLevel(2500);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 4)
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkCoupon3.Checked = true;
                checkCoupon4.Checked = true;
                checkPrice1.Checked = true;
                checkPrice2.Checked = true;
                checkPrice3.Checked = true;
                checkPrice4.Checked = true;
                bounce_linearLayout.Background.SetLevel(3500);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 5)
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkCoupon3.Checked = true;
                checkCoupon4.Checked = true;
                checkCoupon5.Checked = true;
                checkPrice1.Checked = true;
                checkPrice2.Checked = true;
                checkPrice3.Checked = true;
                checkPrice4.Checked = true;
                checkPrice5.Checked = true;
                bounce_linearLayout.Background.SetLevel(4500);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 6)
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkCoupon3.Checked = true;
                checkCoupon4.Checked = true;
                checkCoupon5.Checked = true;
                checkCoupon6.Checked = true;
                bounce_linearLayout.Background.SetLevel(5500);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 7)
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkCoupon3.Checked = true;
                checkCoupon4.Checked = true;
                checkCoupon5.Checked = true;
                checkCoupon6.Checked = true;
                checkCoupon7.Checked = true;
                bounce_linearLayout.Background.SetLevel(6500);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 8)
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkCoupon3.Checked = true;
                checkCoupon4.Checked = true;
                checkCoupon5.Checked = true;
                checkCoupon6.Checked = true;
                checkCoupon7.Checked = true;
                checkCoupon8.Checked = true;
                bounce_linearLayout.Background.SetLevel(7500);
            }
            else if (DealsPageViewModel.clientDeal.ClientDeal.ClientDealDetail[0].DealCount == 9)
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkCoupon3.Checked = true;
                checkCoupon4.Checked = true;
                checkCoupon5.Checked = true;
                checkCoupon6.Checked = true;
                checkCoupon7.Checked = true;
                checkCoupon8.Checked = true;
                checkCoupon9.Checked = true;
                bounce_linearLayout.Background.SetLevel(8500);
            }
            else
            {
                checkCoupon1.Checked = true;
                checkCoupon2.Checked = true;
                checkCoupon3.Checked = true;
                checkCoupon4.Checked = true;
                checkCoupon5.Checked = true;
                checkCoupon6.Checked = true;
                checkCoupon7.Checked = true;
                checkCoupon8.Checked = true;
                checkCoupon9.Checked = true;
                checkCoupon10.Checked = true;
                bounce_linearLayout.Background.SetLevel(9500);
            }

        }
        private async void scanQrCodeButton_Async(object sender, EventArgs e)
        {
            
            if (ActivityCompat.CheckSelfPermission(this.Context, Manifest.Permission.Camera) == Permission.Granted)
            {
                ScanLogic();                
            }
            else
            {
                await AndroidPermissions.checkCameraPermission(this);
                ScanLogic();            

            }
        }
        private void ScanLogic()
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
            var span = new TimeSpan(0,10,0);
            if (timeInterval > span)
            {
                startScanQrCode();
            }
            else 
            {
                showDialog("QR Scan", "Scan after a few minutes", "Okay");
            }
        }
        private async void startScanQrCode()
        {
            var scannedTimings = DateTime.Now;
            DealsPageViewModel.scannedTime = scannedTimings.TimeOfDay;
            string CurrentDate = DateTime.Today.ToString("yyyy-MM-dd");            
            if (DealsViewModel.TimePeriod == 3)
            {
                if ((DateTime.Today.Date <= DateTime.Parse(DealsViewModel.enddate).Date))
                {
                    MobileBarcodeScanner.Initialize(Activity.Application);
                    var scanner = new MobileBarcodeScanner();
                    var result = await scanner.Scan();
                    if (result != null)
                        Console.WriteLine("Scanned Barcode: " + result.Text);
                    if (result != null)
                    {
                        Console.WriteLine("Scanned Barcode: " + result.Text); //ONEWASH
                        if (couponCodes[DealsViewModel.SelectedDealId - 1].ToUpper().Replace(" ", "") == result.Text.ToUpper().Replace(" ", ""))
                        {
                            this.ViewModel.dealId = DealsViewModel.SelectedDealId;
                            this.ViewModel.clientID = CustomerInfo.ClientID;
                            this.ViewModel.Date = DateTime.Today.ToString("yyyy-MM-dd");
                            //_userDialog.Loading();
                            try
                            {
                                await this.ViewModel.AddClientDeals();
                                DealsPageViewModel.scannedTime = DateTime.Now.TimeOfDay;                                
                                validateDeals(false);
                                couponValidity();
                            }
                            catch (Exception ex)
                            {
                                if (ex is OperationCanceledException)
                                {
                                    return;
                                }
                            }
                            //_userDialog.HideLoading();
                        }
                        else
                        {
                            showDialog("Invalid Coupon", "The Scanned Coupon not matched!,Try Again", "Ok");
                        }
                    }
                    else
                    {
                        showDialog("Invalid Coupon", "The Scanned Coupon not matched!,Try Again", "Ok");
                    }
                }
                else
                {
                    showDialog("", "Deal not available", "Ok");

                }

            }
            else
            {
                MobileBarcodeScanner.Initialize(Activity.Application);
                var scanner = new MobileBarcodeScanner();
                var result = await scanner.Scan();
                if (result != null)
                    Console.WriteLine("Scanned Barcode: " + result.Text);
                if (result != null)
                {
                    Console.WriteLine("Scanned Barcode: " + result.Text); //ONEWASH
                    if (couponCodes[DealsViewModel.SelectedDealId - 1].ToUpper().Replace(" ", "") == result.Text.ToUpper().Replace(" ", ""))
                    {
                        this.ViewModel.dealId = DealsViewModel.SelectedDealId;
                        this.ViewModel.clientID = CustomerInfo.ClientID;
                        this.ViewModel.Date = DateTime.Today.ToString("yyyy-MM-dd");
                        //_userDialog.Loading();

                        try
                        {
                            await this.ViewModel.AddClientDeals();
                            DealsPageViewModel.scannedTime  = DateTime.Now.TimeOfDay;                            
                            validateDeals(false);
                            couponValidity();
                        }
                        catch (Exception ex)
                        {
                            if (ex is OperationCanceledException)
                            {
                                return;
                            }
                        }
                        //_userDialog.HideLoading();
                    }
                    else
                    {
                        showDialog("Invalid Coupon", "The Scanned Coupon not matched!,Try Again", "Ok");
                    }
                }
                else
                {


                    showDialog("Invalid Coupon","The Scanned Coupon not matched!,Try Again", "Ok");
                }
            }
           
        }
        private void showDialog(string title,string message,string action)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(Activity);
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetPositiveButton(action, (senderAlert, args) => {
            });

             alert.Show();
        }

    }
    
}
