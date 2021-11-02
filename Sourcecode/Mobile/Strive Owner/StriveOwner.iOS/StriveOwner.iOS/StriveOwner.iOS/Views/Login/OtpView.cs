using System;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.Login
{
    public partial class OtpView : MvxViewController<OTPViewModel>
    {
        public OtpView() : base("OtpView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void TextFieldDidChange(UITextField sender)
        {
            var text = sender.Text;
            if(text.Count() == 1)
            {
                switch(sender.Tag)
                {
                    case 1:
                        SecondDigit.BecomeFirstResponder();
                        break;
                    case 2:
                        ThirdDigit.BecomeFirstResponder();
                        break;
                    case 3:
                        FourthDigit.BecomeFirstResponder();
                        break;
                    case 4:
                        FourthDigit.ResignFirstResponder();
                        break;
                    default:
                        FirstDigit.BecomeFirstResponder();
                        break;
                }
            }
            else
            {
                if (text.Count() == 0)
                    return;
                sender.Text = text.Remove(1);
                sender.ResignFirstResponder();
            }
        }

        void DoInitialSetup()
        {
            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
        }

        partial void VerifyClicked(UIButton sender)
        {
            ViewModel.OTPValue = FirstDigit.Text + SecondDigit.Text + ThirdDigit.Text + FourthDigit.Text;
            ViewModel.VerifyCommand();
        }
    }
}

