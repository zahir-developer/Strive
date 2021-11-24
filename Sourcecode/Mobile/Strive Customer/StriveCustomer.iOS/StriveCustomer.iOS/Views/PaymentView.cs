using System;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;
using Strive.Core.Models.Customer;
using System.Diagnostics;
using System.Threading.Tasks;
using Strive.Core.Utils;
using Newtonsoft.Json;
using Strive.Core.Services.Implementations;
using System.Linq;
using static Strive.Core.ViewModels.Customer.PaymentViewModel;
using System.Collections.Generic;
using Acr.UserDialogs;
using MvvmCross;

namespace StriveCustomer.iOS.Views
{
    public partial class PaymentView : MvxViewController<PaymentViewModel>
    {
       // WeakReference<UITextField> focusedTextField = new(WeakReference<UITextField>);

        UIScrollView scrollView;
        UITextField customerNameTextField;
        UITextField tipAmountTextField;
        UILabel totalAmountDueLabel;
        UILabel paymentInfoLabel;
        UITextField cardNumberTextField;
        UITextField expirationDateTextField;
        UITextField securityCodeTextField;

        UIEdgeInsets scrollViewInsets;
        
        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();
        //
        public float Total=0;

        public PaymentView() : base("PaymentView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            SetupView();
            GetTotal();
            KeyBoardHandling();
            UpdateData();
            
           // RegisterForCardDetailsScanning();

#if DEBUG
            cardNumberTextField.Text = "6011000995500000";
            expirationDateTextField.Text = "12/21";
            securityCodeTextField.Text = "291";
#endif

        }
        public void GetTotal()
        {
            double MembershipAmount = VehicleMembershipViewModel.isDiscoutAvailable ? MembershipDetails.selectedMembershipDetail.DiscountedPrice : MembershipDetails.selectedMembershipDetail.Price;
            var SelectedServices = MembershipDetails.completeList.ServicesWithPrice.Where(x => MembershipDetails.selectedAdditionalServices.Contains(x.ServiceId)).ToList();
            foreach (var Service in SelectedServices)
            {
                if (Service.Price != null)
                {
  
                    Amount += (float)Service.Price;
                    
                }
            }
            Amount += (float)MembershipAmount;
           
        }
       
       

        public long JobID;
        public string Make;
        public string Model;
        public string Color;
        public string ServiceName;
        public string AdditionalServiceName;
        public float Amount;
        public string CustName = CustomerInfo.custName;
      

        public void ShowAlertMsg(string msg, Action okAction = null, bool isCancel = false, string titleTxt = null)
        {
            string title = "Alert";
            string ok = "Ok";
            string cancel = "Cancel";

            if (!string.IsNullOrEmpty(titleTxt))
            {
                title = titleTxt;
            }

            var okAlertController = UIAlertController.Create(title, msg, UIAlertControllerStyle.Alert);
            okAlertController.AddAction(UIAlertAction.Create(ok, UIAlertActionStyle.Default,
                alert =>
                {
                    okAction?.Invoke();
                }));
            if (isCancel)
                okAlertController.AddAction(UIAlertAction.Create(cancel, UIAlertActionStyle.Cancel, null));
            PresentViewController(okAlertController, true, null);
        }

        public async Task PayAsync(string cardNo, string expiryDate, short ccv, float tipAmount)
        {

            //_userDialog.ShowLoading();
            var totalAmnt = Amount + tipAmount;

            if (cardNo.IsEmpty() || expiryDate.IsEmpty() || ccv == 0)
            {
                _userDialog.HideLoading();
                ShowAlertMsg("Please fill card details");
                return;
            }

            try
            {
                var paymentAuthReq = new PaymentAuthReq
                {
                    CardConnect = new Object(),

                    PaymentDetail = new PaymentDetail()
                    {
                        Account = cardNo,
                        Expiry = expiryDate,
                        CCV = ccv,
                        Amount = Amount
                    },

                    BillingDetail = new BillingDetail()
                    {
                        Name = CustomerInfo.customerPersonalInfo.Status[0].FirstName,
                        Address = CustomerInfo.customerPersonalInfo.Status[0].Address1,
                        City = "Chennai",// status.City,
                        Country = "India",//status.Country,
                        Region = "Tamilnadu",//status.State,
                        Postal = CustomerInfo.customerPersonalInfo.Status[0].Zip
                    }

                };


                Debug.WriteLine(JsonConvert.SerializeObject(paymentAuthReq));
                

                var apiService = new PaymentApiService();

                var paymentAuthResponse = await apiService.PaymentAuth(paymentAuthReq);

                // if (paymentAuthResponse.IsSuccess())
                if (paymentAuthResponse.Authcode != null)
                {
                    var paymentCaptureReq = new PaymentCaptureReq
                    {
                        AuthCode = paymentAuthResponse?.Authcode,
                        RetRef = paymentAuthResponse?.Retref,
                        Amount = totalAmnt,
                    };

                    Debug.WriteLine("" + JsonConvert.SerializeObject(paymentCaptureReq));
                    var captureResponse = await apiService.PaymentCapture(paymentCaptureReq);

                    if (captureResponse.Authcode != null)
                    {
                        var generalApiService = new GeneralApiService();
                        var paymentStatusResponse = await generalApiService.GetGlobalData("PAYMENTSTATUS");
                        //Debug.WriteLine("Payment Status Response : " + JsonConvert.SerializeObject(paymentStatusResponse));
                        var paymentStatusId = paymentStatusResponse?.Codes.First(x => x.Name.Equals(PaymentStatus.Success.ToString())).ID ?? -1;

                        var paymentTypeResponse = await generalApiService.GetGlobalData("PAYMENTTYPE");
                        //Debug.WriteLine("Payment Type Response : " + JsonConvert.SerializeObject(paymentTypeResponse));

                        var paymentTypeId = paymentTypeResponse?.Codes.First(x => x.Name.Equals(PaymentType.Card.ToString())).ID ?? -1;

                        var addPaymentReqReq = new AddPaymentReq
                        {
                            SalesPaymentDto = new SalesPaymentDto()
                            {
                                JobPayment = new JobPayment()
                                {
                                    JobID = JobID,
                                    Amount = Amount,
                                    PaymentStatus = paymentStatusId
                                },

                                JobPaymentDetails = new List<JobPaymentDetail>() {
                                            new JobPaymentDetail()
                                            {
                                                Amount = Amount,
                                                PaymentType = paymentTypeId
                                            }
                                        }
                            },
                            LocationID = 1,//AppSettings.LocationID,
                            JobID = JobID
                        };

                        if (tipAmount != 0)
                        {
                            var tipsTypeId = paymentTypeResponse?.Codes.First(x => x.Name.Equals(PaymentType.Tips.ToString(), StringComparison.OrdinalIgnoreCase)).ID ?? -1;

                            var tipAmountObj = new JobPaymentDetail()
                            {
                                Amount = tipAmount,
                                PaymentType = tipsTypeId
                            };

                            addPaymentReqReq.SalesPaymentDto.JobPaymentDetails.Add(tipAmountObj);
                        }

                        //Debug.WriteLine("Add pay req : " + JsonConvert.SerializeObject(addPaymentReqReq));

                        var paymentResponse = await new PaymentApiService().AddPayment(addPaymentReqReq);
                        Debug.WriteLine(JsonConvert.SerializeObject(paymentResponse));
                      

                        if (paymentResponse.Message == "true")
                        {
                            ViewModel.MembershipAgree();
                            
                        }
                        else
                        {
                            _userDialog.HideLoading();
                            ShowAlertMsg("The operation cannot be completed at this time.Unexpected Error!");
                        }
                    }
                    else
                    {
                        _userDialog.HideLoading();
                        ShowAlertMsg("The operation cannot be completed at this time.Unexpected Error!");
                    }
                }
                else
                {
                    _userDialog.HideLoading();
                    ShowAlertMsg("The operation cannot be completed at this time.Unexpected Error!");
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception happened and the reason is : " + ex.Message);
               
            }
            
        }
        
        
        
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        void UpdateData()
        {
            customerNameTextField.Text = CustomerInfo.custName;
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

            var backgroundImage = new UIImageView(UIImage.FromBundle("splash"));
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
            payButton.BackgroundColor = UIColor.Blue; //Common.Colors.APP_BASE_COLOR.ToPlatformColor();
            payButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            payButton.Font = UIFont.SystemFontOfSize(18);
            backgroundView.Add(payButton);

           
            // Clicks
            payButton.TouchUpInside += delegate
            {
                _userDialog.ShowLoading();
                short ccv = 0;
                if (securityCodeTextField.Text != null && securityCodeTextField.Text != "" )
                {
                    ccv = Convert.ToInt16(securityCodeTextField.Text);
                }

                if (tipAmountTextField.Text != null)
                    if (float.TryParse(tipAmountTextField.Text, out float tipAmount))

                        _ =PayAsync(cardNumberTextField.Text, expirationDateTextField.Text, ccv, tipAmount);
               
            };
            
            backgroundImage.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            backgroundImage.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            backgroundImage.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            backgroundImage.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;

            scrollView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor).Active = true;
            scrollView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor).Active = true;
            scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor).Active = true;
            scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor).Active = true;
            scrollView.WidthAnchor.ConstraintEqualTo(337);

            backgroundView.WidthAnchor.ConstraintEqualTo(347);
            backgroundView.HeightAnchor.ConstraintEqualTo(550);
            backgroundView.LeadingAnchor.ConstraintEqualTo(scrollView.LeadingAnchor, constant: 10).Active = true;
            backgroundView.TrailingAnchor.ConstraintEqualTo(scrollView.TrailingAnchor, constant: 4).Active = true;
            backgroundView.TopAnchor.ConstraintGreaterThanOrEqualTo(scrollView.TopAnchor, constant: 60).Active = true;
            backgroundView.CenterYAnchor.ConstraintEqualTo(scrollView.CenterYAnchor).Active = true;
            backgroundView.BottomAnchor.ConstraintEqualTo(scrollView.BottomAnchor, constant: 60).Active = true;
           

            customerNameLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 10).Active = true;
            customerNameLabel.TopAnchor.ConstraintEqualTo(backgroundView.TopAnchor, constant: 30).Active = true;

            
            customerNameTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor, constant:-10).Active = true;
            customerNameTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -8).Active = true;
            customerNameTextField.TopAnchor.ConstraintEqualTo(backgroundView.TopAnchor, constant: 15).Active = true;
            customerNameTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;

            totalAmountDueTitleLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 10).Active = true;
            totalAmountDueTitleLabel.CenterYAnchor.ConstraintEqualTo(totalAmountDueLabel.CenterYAnchor).Active = true;

            totalAmountDueLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            totalAmountDueLabel.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -8).Active = true;
            totalAmountDueLabel.TopAnchor.ConstraintEqualTo(customerNameLabel.BottomAnchor, constant: 20).Active = true;
            totalAmountDueLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            paymentInfoTitleLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 10).Active = true;
            paymentInfoTitleLabel.CenterYAnchor.ConstraintEqualTo(paymentInfoLabel.CenterYAnchor).Active = true;

            paymentInfoLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            paymentInfoLabel.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -8).Active = true;
            paymentInfoLabel.TopAnchor.ConstraintEqualTo(totalAmountDueLabel.BottomAnchor, constant: 20).Active = true;
            paymentInfoLabel.HeightAnchor.ConstraintEqualTo(50).Active = true;

            cardNumberLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 10).Active = true;
            cardNumberLabel.CenterYAnchor.ConstraintEqualTo(cardNumberTextField.CenterYAnchor).Active = true;

            cardNumberTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor,constant: -10).Active = true;
            cardNumberTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -8).Active = true;
            cardNumberTextField.TopAnchor.ConstraintEqualTo(paymentInfoLabel.BottomAnchor, constant: 20).Active = true;
            cardNumberTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;
            cardNumberTextField.WidthAnchor.ConstraintEqualTo(190).Active = true;

            expirationDateLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 10).Active = true;
            expirationDateLabel.CenterYAnchor.ConstraintEqualTo(expirationDateTextField.CenterYAnchor).Active = true;

            expirationDateTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor,constant: -10).Active = true;
            expirationDateTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -8).Active = true;
            expirationDateTextField.TopAnchor.ConstraintEqualTo(cardNumberTextField.BottomAnchor, constant: 20).Active = true;
            expirationDateTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;
           

            securityCodeLabel.LeadingAnchor.ConstraintEqualTo(backgroundView.LeadingAnchor, constant: 10).Active = true;
            securityCodeLabel.CenterYAnchor.ConstraintEqualTo(securityCodeTextField.CenterYAnchor).Active = true;

            securityCodeTextField.LeadingAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor,constant: -10).Active = true;
            securityCodeTextField.TrailingAnchor.ConstraintEqualTo(backgroundView.TrailingAnchor, constant: -8).Active = true;
            securityCodeTextField.TopAnchor.ConstraintEqualTo(expirationDateTextField.BottomAnchor, constant: 20).Active = true;
            securityCodeTextField.HeightAnchor.ConstraintEqualTo(50).Active = true;

            payButton.TopAnchor.ConstraintEqualTo(securityCodeTextField.BottomAnchor, constant: 10).Active = true;
            payButton.BottomAnchor.ConstraintEqualTo(backgroundView.BottomAnchor, constant: 10).Active = true;
            payButton.CenterXAnchor.ConstraintEqualTo(backgroundView.CenterXAnchor).Active = true;
            payButton.HeightAnchor.ConstraintEqualTo(40).Active = true;
            payButton.WidthAnchor.ConstraintEqualTo(240).Active = true;
        }
        void KeyBoardHandling()
        {
            //UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
            UITextField.Notifications.ObserveTextDidBeginEditing(TextFieldBeginEditing);
            UITextField.Notifications.ObserveTextDidEndEditing(TextFieldDidEndEditing);
        }

        void TextFieldBeginEditing(object sender, NSNotificationEventArgs e)
        {
            var textField = e.Notification.Object as UITextField;
            //focusedTextField.SetTarget(textField);
        }

        void TextFieldDidEndEditing(object sender, NSNotificationEventArgs e)
        {
            var textField = e.Notification.Object as UITextField;
            //focusedTextField.SetTarget(textField);
        }

        //void OnKeyboardShow(object sender, UIKeyboardEventArgs e)
        //{
        //    if (focusedTextField.TryGetTarget(out UITextField currentFocusedTextField))
        //    {
        //        var keyboardMinY = e.FrameEnd.GetMinY();
        //        var textFieldOrigin = currentFocusedTextField.ConvertPointToView(currentFocusedTextField.Center, null);

        //        var movingDistance = textFieldOrigin.Y - keyboardMinY;

        //        if (movingDistance > 0)
        //        {
        //            var insets = new UIEdgeInsets(
        //                scrollViewInsets.Top,
        //                scrollViewInsets.Left,
        //                scrollViewInsets.Bottom + movingDistance,
        //                scrollViewInsets.Right);

        //            scrollView.ContentInset = insets;
        //        }
        //        else
        //        {
        //            scrollView.ContentInset = scrollViewInsets;
        //        }

        //        var visibleRect = currentFocusedTextField.ConvertRectToView(currentFocusedTextField.Frame, null);
        //        scrollView.ScrollRectToVisible(visibleRect, true);
        //    }
        //}

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

                    if (tipAmountTextField.Text != null)
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
                    expirationDateTextField.Text = replacedString.Substring(0,2) + "/" + replacedString.Substring(2);
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

