using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using Greeter.Common;
using Greeter.Extensions;
using InfineaSDK.iOS;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Modules.Pay
{
    public partial class PaymentViewController : BaseViewController, IUITextFieldDelegate, IUICollectionViewSource
    {
        WeakReference<UITextField> focusedTextField = new(null);

        UIScrollView scrollView;
        UITextField customerNameTextField;
        UITextField tipAmountTextField;
        UILabel totalAmountDueLabel;
        UILabel paymentInfoLabel;
        UITextField cardNumberTextField;
        UITextField expirationDateTextField;
        UITextField securityCodeTextField;
        UICollectionView tipsTemplatesCv;
        NSLayoutConstraint heightConstraintOfTipsCollectionView;
        public string CardHolderName; 
        UIEdgeInsets scrollViewInsets;

        const string TIP_AMOUNT_FORMAT = "{0}{1}{2}.{3}{4}";

        readonly string[] TIP_TEMPLATES = new string[] { "10", "15", "20", "25", "Custom" };

        int tipTemplateSelectedIndex = -1;

        private IPCDTDevices Peripheral { get; } = IPCDTDevices.Instance;
        private IPCDTDeviceDelegateEvents PeripheralEvents { get; } = new IPCDTDeviceDelegateEvents();
        //private readonly int[] barcodeTone = { 1046, 80, 1397, 80 };

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            KeyBoardHandling();
            UpdateData();
            securityCodeTextField.Hidden = true;
            tipAmountTextField.Text = string.Format(TIP_AMOUNT_FORMAT, 0, 0, 0, 0, 0);

            RegisterForCardDetailsScanning();
            //CardHolderName = ParseMagcardName("%B4799320385017982^K LAKSHMANA/^280722600722000000?;4799320385017982=2807226722?(null)");
#if DEBUG
            //cardNumberTextField.Text = "6011000995500000";
            //expirationDateTextField.Text = "12/21";
            //securityCodeTextField.Text = "291";
#endif
        }

        void RegisterForCardDetailsScanning()
        {
            PeripheralEvents.ConnectionState += OnConnectionStateChanged;

            PeripheralEvents.MagneticCardEncryptedDataTracksDataTrack1maskedTrack2maskedTrack3 += (object sender, MagneticCardEncryptedDataTracksDataTrack1maskedTrack2maskedTrack3EventArgs e) =>
            {
                // e contains Data, Encryption, Track1masked, Track2masked, Track3
                // add handling code here...
                //ScanTypeLabel.Text = "Magcard (Encrypted)";
                Console.WriteLine("MagneticCardEncryptedDataTracksDataTrack1maskedTrack2maskedTrack3");
            };

            PeripheralEvents.MagneticCardReadFailedReason += (object sender, MagneticCardReadFailedReasonEventArgs e) =>
            {
                // e contains Reason and Source  (values explained in LibraryDemo source code)
                // add error handling code here... 
                //ScanTypeLabel.Text = "Magcard (Error)";
                //ScanDataLabel.Text = $"Reason: {e.Reason}";
                Console.WriteLine("MagneticCardReadFailedReason");
            };

            // MSR card swipes
            PeripheralEvents.MagneticCardDataTrack2Track3 += OnMagcardRead;
            ConnectToPeripheral();
            Peripheral.AddDelegate(PeripheralEvents);
        }

        /// <summary>
        /// Plays a sound through the device
        /// </summary>
        /// <returns><c>true</c>, if successful, <c>false</c> otherwise.</returns>
        /// <param name="volume">The sound volume.</param>
        /// <param name="tones">An even-length array of integers, where the odd elements are frequencies and the even elements are durations.</param>
        //public bool PlaySound(int volume, int[] tones)
        //{
        //    try
        //    {
        //        // Attempts to play the sound, providing the volume, the tones, and an output error.
        //        return Peripheral.PlaySound(volume, tones);
        //    }
        //    catch (NSErrorException ex)
        //    {
        //        Console.WriteLine(ex.Error.LocalizedDescription);
        //        //this.DisplayErrorAlert("Play sound failed", ex.Error);
        //        return false;
        //    }
        //}

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

        private void ConnectToPeripheral()
        {
            Console.WriteLine($"InfineaSDK Version {Peripheral.SdkVersionString} built on {Peripheral.SdkBuildDate}");

            // connect to the peripheral - must be called before any further interaction with the peripheral
            Peripheral.Connect();

            // the connection state handler OnConnectionStateChanged will be called for peripheral connection states
            // implement any further peripheral interaction after OnConnectionStateChanged has received ConnStates.ConnConnected
        }

        private void OnMagcardRead(object sender, MagneticCardDataTrack2Track3EventArgs e)
        {
            string data = $"{e.Track1 ?? string.Empty}{e.Track2 ?? string.Empty}{e.Track3 ?? string.Empty}";
            
            //string data = "%B376537649781009^D/NARENDREN ^2310201181064102?(null)(null)";
            Console.WriteLine($"Magcard swiped: {data}");

            //PlaySound(100, barcodeTone);

            Logic.Vibrate(2);

            string cardNumber = ParseMagcardData(data);
            string HolderName = ParseMagcardName(data);
            if (cardNumber == null)
            {
                //ScanTypeLabel.Text = "Magcard (Error)";
                //ScanDataLabel.Text = "Could not parse!";
            }
            else
            {
                //ScanTypeLabel.Text = "Magcard";
                cardNumberTextField.Text = cardNumber;
                CardHolderName = HolderName;
            }
        }

        string ParseMagcardData(string rawData)
        {
            Regex magcardRegex = new Regex(@"^%B(\d+)\^");
            Match numberMatch = magcardRegex.Match(rawData);
            if (!numberMatch.Success)
                return null;
            return numberMatch.Groups[1].Value;
        }

        string ParseMagcardName(string rawData)
        {
            string[] CardData = rawData.Split("^");
            char[] totrim = { '/' };
            string name = CardData[1].Replace(" ","");
            name = name.TrimEnd(totrim);
            name = name.TrimStart(totrim);
            Console.WriteLine(name);
            return CardData[1];
        }
        //string ParseExpiryData(string rawData)
        //{
        //    string BEFORE_EXPIRY_MONTH_YEAR = "/^";

        //    int pos = rawData.IndexOf("/^");

        //    string yearMonth = rawData.Substring(pos + BEFORE_EXPIRY_MONTH_YEAR.Length, 4);

        //    return yearMonth;
        //}

        //string ParseExpiryYear(string rawData)
        //{
        //    string BEFORE_EXPIRY_MONTH_YEAR = "/^";

        //    int pos = rawData.IndexOf("/^");

        //    string yearMonth = rawData.Substring(pos + BEFORE_EXPIRY_MONTH_YEAR.Length, 4);

        //    string year = yearMonth.Substring(0, 2);

        //    return year;
        //}

        //string ParseExpiryMonth(string rawData)
        //{
        //    string BEFORE_EXPIRY_MONTH_YEAR = "/^";

        //    int pos = rawData.IndexOf("/^");

        //    string yearMonth = rawData.Substring(pos + BEFORE_EXPIRY_MONTH_YEAR.Length, 4);

        //    string month = yearMonth.Substring(2, 2);

        //    return month;
        //}

        string ParseExpiryYear(string rawData)
        {
            //string BEFORE_EXPIRY_MONTH_YEAR = "/^";

            int pos = rawData.LastIndexOf("^");

            string yearMonth = rawData.Substring(pos + 1, 4);

            string year = yearMonth.Substring(0, 2);

            return year;
        }

        string ParseExpiryMonth(string rawData)
        {
            //string BEFORE_EXPIRY_MONTH_YEAR = "/^";

            int pos = rawData.LastIndexOf("^");

            string yearMonth = rawData.Substring(pos + 1, 4);

            string month = yearMonth.Substring(2, 2);

            return month;
        }

        void UpdateData()
        {
            customerNameTextField.Text = CustName;
            UpdateAmountLblInDollar(Amount.ToString());
        }

        void UpdateAmountLblInDollar(string amt)
        {
            totalAmountDueLabel.Text = $"${amt}";
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            scrollViewInsets = scrollView.ContentInset;
        }

        void SetupView()
        {
            Title = "Pay";
            NavigationController.NavigationBar.Hidden = false;

            View.AddGestureRecognizer(new UITapGestureRecognizer(DidTapAround));

            var backgroundImage = new UIImageView(UIImage.FromBundle(ImageNames.SPLASH_BG));
            backgroundImage.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundImage.ContentMode = UIViewContentMode.ScaleAspectFill;
            View.Add(backgroundImage);

            scrollView = new UIScrollView(CGRect.Empty);
            scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            View.Add(scrollView);

            var backgroundView = new UIView(CGRect.Empty);
            backgroundView.TranslatesAutoresizingMaskIntoConstraints = false;
            backgroundView.BackgroundColor = UIColor.White;
            backgroundView.Layer.CornerRadius = 5;
            backgroundView.Layer.ShadowColor = UIColor.Black.CGColor;
            backgroundView.Layer.ShadowOpacity = 0.3f;
            backgroundView.Layer.ShadowRadius = 2;
            backgroundView.Layer.ShadowOffset = new CGSize(0, 1);
            scrollView.Add(backgroundView);
            var customerNameLabel = new UILabel(CGRect.Empty);
            customerNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            customerNameLabel.Text = "Customer Name";
            customerNameLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            customerNameLabel.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(customerNameLabel);

            customerNameTextField = new UITextField(CGRect.Empty);
            customerNameTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            customerNameTextField.WeakDelegate = this;
            customerNameTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            customerNameTextField.Font = UIFont.SystemFontOfSize(18);
            customerNameTextField.TextColor = UIColor.Black;
            backgroundView.Add(customerNameTextField);

            var tipAmountNameLabel = new UILabel(CGRect.Empty);
            tipAmountNameLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            tipAmountNameLabel.Text = "Tip Amount($)";
            tipAmountNameLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            tipAmountNameLabel.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(tipAmountNameLabel);

            tipAmountTextField = new UITextField(CGRect.Empty);
            tipAmountTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            tipAmountTextField.WeakDelegate = this;
            tipAmountTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            tipAmountTextField.Font = UIFont.SystemFontOfSize(18);
            tipAmountTextField.TextColor = UIColor.Black;
            tipAmountTextField.KeyboardType = UIKeyboardType.NumberPad;
            backgroundView.Add(tipAmountTextField);

            var cvLayout = new UICollectionViewFlowLayout();
            cvLayout.MinimumInteritemSpacing = 10;
            cvLayout.MinimumLineSpacing = 10;
            cvLayout.SectionInset = new UIEdgeInsets(5, 5, 5, 5);
            cvLayout.ItemSize = UICollectionViewFlowLayout.AutomaticSize;
            cvLayout.EstimatedItemSize = new CGSize(120, 40);
            //cvLayout.ItemSize = new CGSize(120, 40);
            tipsTemplatesCv = new UICollectionView(CGRect.Empty, cvLayout);
            tipsTemplatesCv.TranslatesAutoresizingMaskIntoConstraints = false;
            tipsTemplatesCv.RegisterClassForCell(typeof(TipTemplateCell), nameof(TipTemplateCell));
            tipsTemplatesCv.DataSource = this;
            tipsTemplatesCv.WeakDelegate = this;
            tipsTemplatesCv.BackgroundColor = UIColor.Clear;
            backgroundView.Add(tipsTemplatesCv);

            var totalAmountDueTitleLabel = new UILabel(CGRect.Empty);
            totalAmountDueTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            totalAmountDueTitleLabel.Text = "Total Amount";
            totalAmountDueTitleLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            totalAmountDueTitleLabel.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(totalAmountDueTitleLabel);

            totalAmountDueLabel = new UILabel(CGRect.Empty);
            totalAmountDueLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            totalAmountDueLabel.Font = UIFont.SystemFontOfSize(18);
            totalAmountDueLabel.TextColor = UIColor.Black;
            totalAmountDueLabel.Text = "$15.00";
            backgroundView.Add(totalAmountDueLabel);

            var paymentInfoTitleLabel = new UILabel(CGRect.Empty);
            paymentInfoTitleLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            paymentInfoTitleLabel.Text = "Payment Info";
            paymentInfoTitleLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            paymentInfoTitleLabel.Font = UIFont.BoldSystemFontOfSize(20);
            backgroundView.Add(paymentInfoTitleLabel);

            paymentInfoLabel = new UILabel(CGRect.Empty);
            paymentInfoLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            paymentInfoLabel.Font = UIFont.SystemFontOfSize(18);
            paymentInfoLabel.TextColor = UIColor.Black;
            backgroundView.Add(paymentInfoLabel);

            var cardNumberLabel = new UILabel(CGRect.Empty);
            cardNumberLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            cardNumberLabel.Text = "Card Number";
            cardNumberLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            cardNumberLabel.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(cardNumberLabel);

            cardNumberTextField = new UITextField(CGRect.Empty);
            cardNumberTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            cardNumberTextField.WeakDelegate = this;
            cardNumberTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            cardNumberTextField.Font = UIFont.SystemFontOfSize(18);
            cardNumberTextField.TextColor = UIColor.Black;
            cardNumberTextField.KeyboardType = UIKeyboardType.NumberPad;
            backgroundView.Add(cardNumberTextField);

            var expirationDateLabel = new UILabel(CGRect.Empty);
            expirationDateLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            expirationDateLabel.Text = "Expiration Date";
            expirationDateLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            expirationDateLabel.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(expirationDateLabel);

            expirationDateTextField = new UITextField(CGRect.Empty);
            expirationDateTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            expirationDateTextField.WeakDelegate = this;
            expirationDateTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            expirationDateTextField.Font = UIFont.SystemFontOfSize(18);
            expirationDateTextField.TextColor = UIColor.Black;
            expirationDateTextField.Placeholder = "mm/yy";
            backgroundView.Add(expirationDateTextField);

            //var securityCodeLabel = new UILabel(CGRect.Empty);
            //securityCodeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            //securityCodeLabel.Text = "Security Code(CVV)";
            //securityCodeLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            //securityCodeLabel.Font = UIFont.SystemFontOfSize(18);
            //backgroundView.Add(securityCodeLabel);

            securityCodeTextField = new UITextField(CGRect.Empty);
            securityCodeTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            securityCodeTextField.WeakDelegate = this;
            securityCodeTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            securityCodeTextField.Font = UIFont.SystemFontOfSize(18);
            securityCodeTextField.TextColor = UIColor.Black;
            securityCodeTextField.KeyboardType = UIKeyboardType.NumberPad;
            securityCodeTextField.SecureTextEntry = true;
            backgroundView.Add(securityCodeTextField);
            securityCodeTextField.Hidden = true;

            var payButton = new UIButton(CGRect.Empty);
            payButton.TranslatesAutoresizingMaskIntoConstraints = false;
            payButton.SetTitle("Pay", UIControlState.Normal);
            payButton.BackgroundColor = ColorConverters.FromHex(Common.Colors.APP_BASE_COLOR).ToPlatformColor();
            payButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            payButton.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(payButton);

            // Clicks
            payButton.TouchUpInside += delegate
            {
                //short ccv = 0;
                //if (!securityCodeTextField.Text.IsEmpty())
                //    ccv = Convert.ToInt16(securityCodeTextField.Text);

                if (!tipAmountTextField.Text.IsEmpty())
                    if (float.TryParse(tipAmountTextField.Text, out float tipAmount))

                        _ = PayAsync(CardNumber, expirationDateTextField.Text, tipAmount, CardHolderName);
            };

            backgroundImage.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            backgroundImage.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            backgroundImage.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            backgroundImage.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            scrollView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            scrollView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            backgroundView.LeadingAnchor.ConstraintEqualTo(scrollView.LeadingAnchor, constant: 60).Active = true;
            backgroundView.TrailingAnchor.ConstraintEqualTo(scrollView.TrailingAnchor, constant: -60).Active = true;
            backgroundView.TopAnchor.ConstraintGreaterThanOrEqualTo(scrollView.TopAnchor, constant: 60).Active = true;
            backgroundView.CenterYAnchor.ConstraintEqualTo(scrollView.CenterYAnchor).Active = true;
            backgroundView.BottomAnchor.ConstraintEqualTo(scrollView.BottomAnchor, constant: -60).Active = true;
            backgroundView.WidthAnchor.ConstraintEqualTo(scrollView.WidthAnchor, constant: -120).Active = true;
            var heightConstraint = backgroundView.HeightAnchor.ConstraintEqualTo(scrollView.HeightAnchor, constant: -120);
            heightConstraint.Priority = 249;
            heightConstraint.Active = true;

            customerNameLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 100).Active = true;
            customerNameLabel.TopAnchor.ConstraintEqualTo(backgroundView.TopAnchor, constant: 60).Active = true;

            customerNameTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            customerNameTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            customerNameTextField.TopAnchor.ConstraintEqualTo(backgroundView.TopAnchor, constant: 50).Active = true;
            customerNameTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;

            tipAmountNameLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 100).Active = true;
            //tipAmountNameLabel.CenterYAnchor.ConstraintEqualTo(tipAmountTextField.CenterYAnchor).Active = true;
            tipAmountNameLabel.TopAnchor.ConstraintEqualTo(customerNameTextField.BottomAnchor, constant: 30).Active = true;

            tipsTemplatesCv.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            tipsTemplatesCv.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            tipsTemplatesCv.TopAnchor.ConstraintEqualTo(tipAmountNameLabel.TopAnchor, constant: 0).Active = true;

            heightConstraintOfTipsCollectionView = tipsTemplatesCv.HeightAnchor.ConstraintEqualTo(100);
            heightConstraintOfTipsCollectionView.Active = true;

            tipAmountTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            tipAmountTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            //tipAmountTextField.TopAnchor.ConstraintEqualTo(customerNameTextField.BottomAnchor, constant: 30).Active = true;
            tipAmountTextField.TopAnchor.ConstraintEqualTo(tipsTemplatesCv.BottomAnchor, constant: 10).Active = true;
            tipAmountTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;

            totalAmountDueTitleLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 100).Active = true;
            totalAmountDueTitleLabel.CenterYAnchor.ConstraintEqualTo(totalAmountDueLabel.CenterYAnchor).Active = true;

            totalAmountDueLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            totalAmountDueLabel.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            totalAmountDueLabel.TopAnchor.ConstraintEqualTo(tipAmountTextField.BottomAnchor, constant: 30).Active = true;
            totalAmountDueLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            paymentInfoTitleLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 100).Active = true;
            paymentInfoTitleLabel.CenterYAnchor.ConstraintEqualTo(paymentInfoLabel.CenterYAnchor).Active = true;

            paymentInfoLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            paymentInfoLabel.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            paymentInfoLabel.TopAnchor.ConstraintEqualTo(totalAmountDueLabel.BottomAnchor, constant: 30).Active = true;
            paymentInfoLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            cardNumberLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 100).Active = true;
            cardNumberLabel.CenterYAnchor.ConstraintEqualTo(cardNumberTextField.CenterYAnchor).Active = true;

            cardNumberTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            cardNumberTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            cardNumberTextField.TopAnchor.ConstraintEqualTo(paymentInfoLabel.BottomAnchor, constant: 30).Active = true;
            cardNumberTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;

            expirationDateLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 100).Active = true;
            expirationDateLabel.CenterYAnchor.ConstraintEqualTo(expirationDateTextField.CenterYAnchor).Active = true;

            expirationDateTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            expirationDateTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            expirationDateTextField.TopAnchor.ConstraintEqualTo(cardNumberTextField.BottomAnchor, constant: 30).Active = true;
            expirationDateTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;

            //securityCodeLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 100).Active = true;
            //securityCodeLabel.CenterYAnchor.ConstraintEqualTo(securityCodeTextField.CenterYAnchor).Active = true;

            securityCodeTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            securityCodeTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            securityCodeTextField.TopAnchor.ConstraintEqualTo(expirationDateTextField.BottomAnchor, constant: 30).Active = true;
            securityCodeTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;

            payButton.TopAnchor.ConstraintEqualTo(securityCodeTextField.BottomAnchor, constant: 50).Active = true;
            payButton.BottomAnchor.ConstraintEqualTo(backgroundView.BottomAnchor, constant: -60).Active = true;
            payButton.CenterXAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            payButton.HeightAnchor.ConstraintEqualTo(50).Active = true;
            payButton.WidthAnchor.ConstraintEqualTo(250).Active = true;
        }

        //void NavigateToPaymentSuccessScreen()
        //{
        //    var vc = GetHomeStorybpard().InstantiateViewController(nameof(PaymentSucessViewController));
        //    NavigateToWithAnim(vc);
        //}

        void KeyBoardHandling()
        {
            UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
            UITextField.Notifications.ObserveTextDidBeginEditing(TextFieldBeginEditing);
            UITextField.Notifications.ObserveTextDidEndEditing(TextFieldDidEndEditing);
        }

        void TextFieldBeginEditing(object sender, NSNotificationEventArgs e)
        {
            var textField = e.Notification.Object as UITextField;
            focusedTextField.SetTarget(textField);
        }

        void TextFieldDidEndEditing(object sender, NSNotificationEventArgs e)
        {
            var textField = e.Notification.Object as UITextField;
            focusedTextField.SetTarget(textField);
        }

        void OnKeyboardShow(object sender, UIKeyboardEventArgs e)
        {
            if (focusedTextField.TryGetTarget(out UITextField currentFocusedTextField))
            {
                var keyboardMinY = e.FrameEnd.GetMinY();
                var textFieldOrigin = currentFocusedTextField.ConvertPointToView(currentFocusedTextField.Center, null);

                var movingDistance = textFieldOrigin.Y - keyboardMinY;

                if (movingDistance > 0)
                {
                    var insets = new UIEdgeInsets(
                        scrollViewInsets.Top,
                        scrollViewInsets.Left,
                        scrollViewInsets.Bottom + movingDistance,
                        scrollViewInsets.Right);

                    scrollView.ContentInset = insets;
                }
                else
                {
                    scrollView.ContentInset = scrollViewInsets;
                }

                var visibleRect = currentFocusedTextField.ConvertRectToView(currentFocusedTextField.Frame, null);
                scrollView.ScrollRectToVisible(visibleRect, true);
            }
        }

        void UpdateTipsCollectionViewHeight()
        {
            tipsTemplatesCv.LayoutIfNeeded();
            var height = tipsTemplatesCv.CollectionViewLayout.CollectionViewContentSize.Height;
            heightConstraintOfTipsCollectionView.Constant = height;
            tipsTemplatesCv.LayoutIfNeeded();
        }

        void OnKeyboardHide(object sender, UIKeyboardEventArgs e)
        {
            scrollView.ContentInset = scrollViewInsets;
            scrollView.ScrollsToTop = true;
        }

        void DidTapAround()
        {
            View.EndEditing(true);
        }

        [Export("textField:shouldChangeCharactersInRange:replacementString:")]
        public bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            if (textField == tipAmountTextField)
            {
                if (replacementString.Length > 1) return false;

                var oldNSString = new NSString(tipAmountTextField.Text ?? string.Empty);

                var replacedString = oldNSString.Replace(range, new NSString(replacementString))
                    .ToString()
                    .Replace(".", string.Empty);

                if (int.TryParse(replacedString, out int tipAmount))
                {
                    var formattedString = tipAmount.ToString("D" + 5.ToString())
                        .Insert(3, ".");

                    if (formattedString.Length > 6) return false;

                    tipAmountTextField.Text = formattedString;

                    if (!tipAmountTextField.Text.IsEmpty())
                        if (float.TryParse(tipAmountTextField.Text, out float extraTipAmount))
                        {
                            UpdateAmountLblInDollar((Amount + extraTipAmount).ToString());
                        }
                }

                return false;
            }

            if (textField == cardNumberTextField)
            {
                var oldNSString = new NSString(cardNumberTextField.Text ?? string.Empty);
                var replacedString = oldNSString.Replace(range, new NSString(replacementString));

                string parsedCardNo = ParseMagcardData(replacementString);

                if (!string.IsNullOrEmpty(parsedCardNo))
                {
                    replacedString = (NSString)parsedCardNo;
                    string year = ParseExpiryYear(replacementString);
                    string month = ParseExpiryMonth(replacementString);
                    CardHolderName = ParseMagcardName(replacementString);
                    //string yearMonth = ParseExpiryData(replacementString);

                    string monthYear = month + year;
                    UpdateExpiryField(monthYear);
                }

                if (replacedString.Length > 16)
                {
                    return false;
                }

                if(!string.IsNullOrEmpty(parsedCardNo))
                {
                    CardNumber = replacedString;
                }
                else
                {
                    if (string.IsNullOrEmpty(CardNumber))
                        CardNumber = cardNumberTextField.Text ?? string.Empty;

                    var replacedNo = ((NSString)CardNumber).Replace(range, new NSString(replacementString));
                    CardNumber = replacedNo;
                }

                if (replacedString.Length > 11)
                {
                    string stars = string.Empty;

                    var shownCountOfCardNumber = 4;

                    for (int i = 0; i < replacedString.Length - shownCountOfCardNumber; i++)
                    {
                        stars += "*";
                    }

                    var maskedCardNumber = stars + replacedString.ToString().Substring((int)replacedString.Length - shownCountOfCardNumber, shownCountOfCardNumber);

                    cardNumberTextField.Text = maskedCardNumber;

                    return false;
                }
                else
                {
                    string stars = string.Empty;
                    for (int i = 0; i < replacedString.Length; i++)
                    {
                        stars += "*";
                    }
                    cardNumberTextField.Text = stars;

                    return false;
                }
            }

            if (textField == securityCodeTextField)
            {
                var oldNSString = new NSString(securityCodeTextField.Text ?? string.Empty);
                var replacedString = oldNSString.Replace(range, new NSString(replacementString));

                return replacedString.Length <= 3;
            }

            if (textField == expirationDateTextField)
            {
                if (replacementString.Length > 1) return false;

                var oldNSString = new NSString(expirationDateTextField.Text ?? string.Empty);
                var replacedString = oldNSString.Replace(range, new NSString(replacementString))
                    .ToString()
                    .Replace("/", string.Empty);

                if (replacedString.Length > 4)
                    return false;

                if (replacedString.Length > 2)
                {
                    expirationDateTextField.Text = $"{replacedString[..2]}/{replacedString[2..]}";
                    return false;
                }
            }

            return true;
        }

        void UpdateExpiryField(string replacedString)
        {
            if (replacedString.Length > 4)
                return;

            if (replacedString.Length > 2)
            {
                expirationDateTextField.Text = $"{replacedString[..2]}/{replacedString[2..]}";
            }
            else
            {
                expirationDateTextField.Text = replacedString;
            }
        }

        void TipTemplateSelected(int pos)
        {
            var indexPaths = new List<NSIndexPath>();
            if (tipTemplateSelectedIndex != -1)
            {
                indexPaths.Add(NSIndexPath.FromRowSection(tipTemplateSelectedIndex, 0));
            }

            indexPaths.Add(NSIndexPath.FromRowSection(pos, 0));

            tipTemplateSelectedIndex = pos;

            var indexPathsArray = indexPaths.ToArray();

            UIView.Animate(0.2, () => {
                tipsTemplatesCv.ReloadItems(indexPathsArray);
            });

            //TODO : update tip amount
            if (TIP_TEMPLATES[pos].Equals("custom", StringComparison.CurrentCultureIgnoreCase))
            {
                tipAmountTextField.UserInteractionEnabled = true;
                tipAmountTextField.Text = string.Format(TIP_AMOUNT_FORMAT, 0, 0, 0, 0, 0);
                UpdateAmountLblInDollar(Amount.ToString());
            }
            else
            {
                var percentage = Convert.ToInt32(TIP_TEMPLATES[pos]);
                var tipAmount = (Amount * percentage) / 100;
                tipAmountTextField.UserInteractionEnabled = false;

                //var formattedString = tipAmount.ToString("D" + 5.ToString())
                //     .Insert(3, ".");

                //if (formattedString.Length > 6) return;

                //tipAmountTextField.Text = tipAmount.ToString();

                tipAmountTextField.Text = String.Format("{0:0.00}", tipAmount);

                if (!tipAmountTextField.Text.IsEmpty())
                    if (float.TryParse(tipAmountTextField.Text, out float extraTipAmount))
                    {
                        UpdateAmountLblInDollar((Amount + extraTipAmount).ToString());
                    }
            }
        }

        [Export("textFieldShouldReturn:")]
        public bool ShouldReturn(UITextField textField)
        {
            if (textField == customerNameTextField)
            {
                tipAmountTextField.BecomeFirstResponder();
            }
            else if (textField == tipAmountTextField)
            {
                cardNumberTextField.BecomeFirstResponder();
            }
            else if (textField == cardNumberTextField)
            {
                expirationDateTextField.BecomeFirstResponder();
            }
            else if (textField == expirationDateTextField)
            {
                securityCodeTextField.BecomeFirstResponder();
            }
            else
            {
                View.EndEditing(true);
            }
            return true;
        }

        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return TIP_TEMPLATES.Length;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (TipTemplateCell)collectionView.DequeueReusableCell(nameof(TipTemplateCell), indexPath);
            bool isSelectedPos = false;
            if (tipTemplateSelectedIndex == indexPath.Row)
            {
                isSelectedPos = true;
            }

            cell.UpdateTipTemplate(TIP_TEMPLATES[indexPath.Row], TipTemplateSelected, isSelectedPos, indexPath.Row);
            return cell;
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            UpdateTipsCollectionViewHeight();
        }
    }
}
