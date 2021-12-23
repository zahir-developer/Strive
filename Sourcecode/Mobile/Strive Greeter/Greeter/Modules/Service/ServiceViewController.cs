// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Modules.Service;
using Greeter.Services.Api;
using Greeter.Storyboards;
using InfineaSDK.iOS;
using UIKit;

namespace Greeter
{
    public partial class ServiceViewController : BaseViewController, IUITextFieldDelegate
    {
        // Static Data
        readonly UIColor unselectedBtnBgColor = UIColor.FromRGB(204, 255, 248);
        readonly UIColor unselectedBtnTxtColor = UIColor.Black;
        readonly UIColor selectedBtnBgColor = UIColor.FromRGB(1, 100, 87);
        readonly UIColor selectedBtnTxtColor = UIColor.White;

        // State Values
        bool IsWash = true;

        //BarcodeResponse barcodeResponse;

        private IPCDTDevices Peripheral { get; } = IPCDTDevices.Instance;
        private IPCDTDeviceDelegateEvents PeripheralEvents { get; } = new IPCDTDeviceDelegateEvents();

        public ServiceViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Initial UI Settings
            Initialise();
            UpdateStaticDataToUI();

            //_ = GetAvailableSchedules();

            //Clicks
            btnWash.TouchUpInside += delegate
            {
                IsWash = true;
                UnSelectButton(btnDetail);
                SelectButton(btnWash);
            };

            btnDetail.TouchUpInside += delegate
            {
                IsWash = false;
                UnSelectButton(btnWash);
                SelectButton(btnDetail);
            };

            btnCloseBarcode.TouchUpInside += delegate
            {
                EmptyTextField(txtFieldBarcode);
            };

            btnCancel.TouchUpInside += delegate
            {
                GoBackWithAnimation();
            };

            btnSelect.TouchUpInside += delegate
            {
                if (txtFieldBarcode.Text.IsEmpty())
                {
                    ShowAlertMsg(Common.Messages.BARCODE_EMPTY);
                }
                else
                {
                    _ = GetBarcodeDetails(txtFieldBarcode.Text);
                }
            };

            btnDriveUp.TouchUpInside += delegate
            {
                NavigateToWashOrDetailScreen(null, txtFieldBarcode.Text);
            };

            lblChangeloc.AddGestureRecognizer(new UITapGestureRecognizer(LocationTap));
            lblLastService.AddGestureRecognizer(new UITapGestureRecognizer(LastServiceTap));
            lblViewIssue.AddGestureRecognizer(new UITapGestureRecognizer(ViewIssueTap));

            RegisterForBarcodeScanning();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            _ = GetAndUpdateLocationWashTimeToUI();
        }

        async Task GetAndUpdateLocationWashTimeToUI()
        {
            var washTime = await GetWashTime(AppSettings.LocationID);

            //if (washTime != 0)
            //{
                AppSettings.WashTime = washTime;
            //}
            //else
            //{
            //    ShowAlertMsg("Wash Time not Receiving from Api");
            //}

            UpdateWashTimeToUI(washTime);
        }

        void UpdateWashTimeToUI(int washTime)
        {
            lblWashTime.Text = washTime.ToString() + ":00";
        }

        async Task<int> GetWashTime(long locationId)
        {
            ShowActivityIndicator();
            var response = await new GeneralApiService().GetLocationWashTime(locationId);
            HideActivityIndicator();

            HandleResponse(response);

            int washTime = 0;

            if (response.IsSuccess())
            {
                washTime = response.Locations[0].WashTimeMinutes;
            }

            return washTime;
        }

        void Initialise()
        {
            txtFieldBarcode.AddLeftPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            txtFieldBarcode.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);

            txtFieldBarcode.WeakDelegate = this;

            DismissKeyboardOnTapArround = true;

#if DEBUG
            //txtFieldBarcode.Text = "ZNL9678";
            //txtFieldBarcode.Text = "61012381";
            //txtFieldBarcode.Text = "63010412";
            //txtFieldBarcode.Text = "73077401";
            //txtFieldBarcode.Text = "231120211";
            //txtFieldBarcode.Text = "22112021";
            //txtFieldBarcode.Text = "05996829";
            //txtFieldBarcode.Text = "291120211";
            //txtFieldBarcode.Text = "73062069";
            //txtFieldBarcode.Text = "73051914";
            //txtFieldBarcode.Text = "73075933";
            //txtFieldBarcode.Text = "60022800";
            txtFieldBarcode.Text = "60023615";

            // DEV Membership Barcode
            //txtFieldBarcode.Text = "19112021p";
            //txtFieldBarcode.Text = "61003352";

            //QA Membership Barcode
            //txtFieldBarcode.Text = "16112021";
            //txtFieldBarcode.Text = "03112021";
            //txtFieldBarcode.Text = "73085420";

#endif
        }

        void UpdateStaticDataToUI()
        {
            lblLocName.Text = AppSettings.LocationName;
        }

        void RegisterForBarcodeScanning()
        {
            PeripheralEvents.ConnectionState += OnConnectionStateChanged;
            PeripheralEvents.BarcodeNSDataType += OnBarcodeScanned;
            ConnectToPeripheral();
            Peripheral.AddDelegate(PeripheralEvents);
        }

        [Export("textFieldShouldReturn:")]
        public bool ShouldReturn(UITextField textField)
        {
            textField.EndEditing(true);
            return true;
        }

        async Task GetBarcodeDetails(string barcode)
        {
            ShowActivityIndicator();
            var response = await new VehicleApiService().GetBarcode(barcode);
            HideActivityIndicator();

            HandleResponse(response);

            if (response.IsSuccess() && response.ClientAndVehicleDetailList != null && response.ClientAndVehicleDetailList.Count > 0)
            {
                //barcodeResponse = response;
                txtFieldBarcode.Text = string.Empty;
                NavigateToWashOrDetailScreen(response.ClientAndVehicleDetailList[0], barcode);
            }
            else
                ShowAlertMsg(Common.Messages.BARCODE_WRONG);
        }

        private void LocationTap()
        {
            UIViewController vc = UIStoryboard.FromName(StoryBoardNames.USER, null)
                                 .InstantiateViewController(nameof(LocationViewController));

            TabBarController.NavigationController.SetViewControllers(new UIViewController[] { vc }, true);
        }

        void LastServiceTap(UITapGestureRecognizer tap)
        {
            if (txtFieldBarcode.Text.IsEmpty())
            {
                ShowAlertMsg(Common.Messages.BARCODE_EMPTY);
                return;
            }

            _ = CheckBarcodeAndNavigateToLastServicecAsync(txtFieldBarcode.Text);

            //Print(string.Empty);
        }

        async Task CheckBarcodeAndNavigateToLastServicecAsync(string barcode)
        {
            var clientAndVehicleDetail = await CheckBarcodeAsync(barcode);
            if (clientAndVehicleDetail is not null)
            {
                txtFieldBarcode.Text = string.Empty;
                NavigateToLastService(clientAndVehicleDetail);
            }
        }

        async Task<ClientAndVehicleDetail> CheckBarcodeAsync(string barcode)
        {
            ShowActivityIndicator();
            var response = await SingleTon.VehicleApiService.GetBarcode(txtFieldBarcode.Text);
            HideActivityIndicator();

            HandleResponse(response);

            ClientAndVehicleDetail clientAndVehicleDetail = null;

            if (response.IsSuccess() && response.ClientAndVehicleDetailList != null && response.ClientAndVehicleDetailList.Count > 0)
            {
                clientAndVehicleDetail = response.ClientAndVehicleDetailList[0];
            }
            else
                ShowAlertMsg(Common.Messages.BARCODE_WRONG);

            return clientAndVehicleDetail;
        }

        void ViewIssueTap()
        {
            //if (txtFieldBarcode.Text.IsEmpty())
            //{
            //    ShowAlertMsg(Common.Messages.BARCODE_EMPTY);
            //    return;
            //}

            NavigateToIssue();
        }

        void NavigateToLastService(ClientAndVehicleDetail clientAndVehicleDetail)
        {
            NavigateToWithAnim(new LastVisitViewController(clientAndVehicleDetail.VehicleID));
        }

        void NavigateToIssue()
        {
            var vc = this.Storyboard.InstantiateViewController(nameof(IssuesViewController));
            NavigateToWithAnim(vc);
        }

        void NavigateToWashOrDetailScreen(ClientAndVehicleDetail clientDetail = null, string barcode = null)
        {
            //if (IsWash)
            //    ShowAlertMsg(btnWash.TitleLabel.Text);
            //else
            //    ShowAlertMsg(btnDetail.TitleLabel.Text);

            //UIStoryboard sb = UIStoryboard.FromName("", null);

            var vc = (ServiceQuestionViewController)this.Storyboard.InstantiateViewController(nameof(ServiceQuestionViewController));

            vc.Barcode = barcode;
            if (clientDetail is null)
            {
                vc.IsNewBarcode = true;
            }
            else if (clientDetail != null)
            {
                //var clientDetail = barcodeResponse.ClientAndVehicleDetailList[0];
                vc.MakeID = clientDetail.MakeID;
                vc.ModelID = clientDetail.ModelID;
                vc.Model = clientDetail.Model;

                vc.ColorID = clientDetail.ColorID;
                vc.ClientID = clientDetail.ClientID;
                vc.VehicleID = clientDetail.VehicleID;
                vc.CustName = clientDetail.FirstName + " " + clientDetail.LastName;
                vc.UpchargeID = clientDetail.UpchargeID;
            }

            if (IsWash)
            {
                vc.ServiceType = ServiceType.Wash;
            }
            else
            {
                vc.ServiceType = ServiceType.Detail;
            }

            NavigateToWithAnim(vc);
        }

        void UnSelectButton(UIButton btn)
        {
            btn.BackgroundColor = unselectedBtnBgColor;
            SetTextColor(btn, unselectedBtnTxtColor);
        }

        void SelectButton(UIButton btn)
        {
            btn.BackgroundColor = selectedBtnBgColor;
            SetTextColor(btn, selectedBtnTxtColor);
        }

        void EmptyTextField(UITextField txtField)
        {
            txtField.Text = string.Empty;
        }

        void SetTextColor(UIButton btn, UIColor color)
        {
            btn.SetTitleColor(color, UIControlState.Normal);
        }

        private void ConnectToPeripheral()
        {
            Console.WriteLine($"InfineaSDK Version {Peripheral.SdkVersionString} built on {Peripheral.SdkBuildDate}");

            // connect to the peripheral - must be called before any further interaction with the peripheral
            Peripheral.Connect();

            // the connection state handler OnConnectionStateChanged will be called for peripheral connection states
            // implement any further peripheral interaction after OnConnectionStateChanged has received ConnStates.ConnConnected
        }

        private void OnConnectionStateChanged(object sender, ConnectionStateEventArgs e)
        {
            switch (e.State)
            {
                case ConnStates.ConnDisconnected:
                    Console.WriteLine("Peripheral disconnected");
                    //View.BackgroundColor = disconnectedColor;
                    //ConnectionLabel.Text = "Peripheral disconnected";

                    //RfidButton.Hidden = true;
                    //EmvButton.Hidden = true;
                    //ConnectButton.Hidden = false;
                    break;

                case ConnStates.ConnConnecting:
                    Console.WriteLine("Peripheral connecting...");
                    //View.BackgroundColor = connectingColor;
                    //ConnectionLabel.Text = "Peripheral connecting";
                    //RfidButton.Hidden = true;
                    //EmvButton.Hidden = true;
                    //ConnectButton.Hidden = true;
                    break;

                case ConnStates.ConnConnected:
                    Console.WriteLine("Peripheral connected");
                    //View.BackgroundColor = connectedColor;
                    //ConnectionLabel.Text = "Peripheral connected!";
                    //RfidButton.Hidden = false;
                    //EmvButton.Hidden = false;
                    //ConnectButton.Hidden = true;
                    //UpdateBatteryPercentage();
                    //CheckEmsrKeys();
                    break;
            }
        }

        private void OnBarcodeScanned(object sender, BarcodeNSDataTypeEventArgs e)
        {
            Console.WriteLine($"Barcode scanned: {e.Barcode} ({e.Type})");
            //ScanTypeLabel.Text = $"Barcode ({e.Type})";
            txtFieldBarcode.Text = e.Barcode.ToString();
        }
    }
}
