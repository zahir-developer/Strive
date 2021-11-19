using System;
using System.Text.RegularExpressions;
using CoreGraphics;
using Foundation;
using Greeter.Common;
using Greeter.Extensions;
using Greeter.Storyboards;
using InfineaSDK.iOS;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Modules.Pay
{
    public partial class PaymentViewController : BaseViewController, IUITextFieldDelegate
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

        UIEdgeInsets scrollViewInsets;

        const string TIP_AMOUNT_FORMAT = "{0}{1}{2}.{3}{4}";

        private IPCDTDevices Peripheral { get; } = IPCDTDevices.Instance;
        private IPCDTDeviceDelegateEvents PeripheralEvents { get; } = new IPCDTDeviceDelegateEvents();
        private readonly int[] barcodeTone = { 1046, 80, 1397, 80 };

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupView();
            KeyBoardHandling();
            UpdateData();

            tipAmountTextField.Text = string.Format(TIP_AMOUNT_FORMAT, 0, 0, 0, 0, 0);

            RegisterForCardDetailsScanning();

            #if DEBUG
                cardNumberTextField.Text = "6011000995500000";
                expirationDateTextField.Text = "12/21";
                securityCodeTextField.Text = "291";
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

            Console.WriteLine($"Magcard swiped: {data}");

            //PlaySound(100, barcodeTone);

            Logic.Vibrate(2);

            string cardNumber = ParseMagcardData(data);
            if (cardNumber == null)
            {
                //ScanTypeLabel.Text = "Magcard (Error)";
                //ScanDataLabel.Text = "Could not parse!";
            }
            else
            {
                //ScanTypeLabel.Text = "Magcard";
                cardNumberTextField.Text = cardNumber;
            }
        }

        private static string ParseMagcardData(string rawData)
        {
            Regex magcardRegex = new Regex(@"^%B(\d+)\^");
            Match numberMatch = magcardRegex.Match(rawData);
            if (!numberMatch.Success)
                return null;
            return numberMatch.Groups[1].Value;
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
            //totalAmountDueLabel.Text = "$15.00";
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

            var securityCodeLabel = new UILabel(CGRect.Empty);
            securityCodeLabel.TranslatesAutoresizingMaskIntoConstraints = false;
            securityCodeLabel.Text = "Security Code(CVV)";
            securityCodeLabel.TextColor = UIColor.FromRGB(36.0f / 255.0f, 72.0f / 255.0f, 154.0f / 255.0f);
            securityCodeLabel.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(securityCodeLabel);

            securityCodeTextField = new UITextField(CGRect.Empty);
            securityCodeTextField.TranslatesAutoresizingMaskIntoConstraints = false;
            securityCodeTextField.WeakDelegate = this;
            securityCodeTextField.BorderStyle = UITextBorderStyle.RoundedRect;
            securityCodeTextField.Font = UIFont.SystemFontOfSize(18);
            securityCodeTextField.TextColor = UIColor.Black;
            securityCodeTextField.KeyboardType = UIKeyboardType.NumberPad;
            securityCodeTextField.SecureTextEntry = true;
            backgroundView.Add(securityCodeTextField);

            var payButton = new UIButton(CGRect.Empty);
            payButton.TranslatesAutoresizingMaskIntoConstraints = false;
            payButton.SetTitle("Pay", UIControlState.Normal);
            payButton.BackgroundColor = Common.Colors.APP_BASE_COLOR.ToPlatformColor();
            payButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            payButton.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(payButton);

            // Clicks
            payButton.TouchUpInside += delegate
            {
                short ccv = 0;
                if (!securityCodeTextField.Text.IsEmpty())
                    ccv = Convert.ToInt16(securityCodeTextField.Text);

                if (!tipAmountTextField.Text.IsEmpty())
                    if (float.TryParse(tipAmountTextField.Text, out float tipAmount))

                        _ = PayAsync(cardNumberTextField.Text, expirationDateTextField.Text, ccv, tipAmount);
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
            tipAmountNameLabel.CenterYAnchor.ConstraintEqualTo(tipAmountTextField.CenterYAnchor).Active = true;

            tipAmountTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            tipAmountTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -100).Active = true;
            tipAmountTextField.TopAnchor.ConstraintEqualTo(customerNameTextField.BottomAnchor, constant: 30).Active = true;
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

            securityCodeLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 100).Active = true;
            securityCodeLabel.CenterYAnchor.ConstraintEqualTo(securityCodeTextField.CenterYAnchor).Active = true;

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

        void NavigateToPaymentSuccessScreen()
        {
            var vc = GetHomeStorybpard().InstantiateViewController(nameof(PaymentSucessViewController));
            NavigateToWithAnim(vc);
        }

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

                return replacedString.Length <= 16;
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
    }
}
