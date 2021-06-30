using System;
using CoreGraphics;
using Greeter.Common;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.Extensions
{
    public static class UIExtension
    {
        public static void AddLeftPadding(this UITextField tf, float padding)
        {
            var paddingView = new UIView(frame: new CoreGraphics.CGRect(0, 0, padding, tf.Frame.Height));
            tf.LeftView = paddingView;
            tf.LeftViewMode = UITextFieldViewMode.Always;
        }

        public static void AddRightPadding(this UITextField txtField, float padding)
        {
            var paddingView = new UIView(frame: new CoreGraphics.CGRect(0, 0, padding, txtField.Frame.Height));
            txtField.RightView = paddingView;
            txtField.RightViewMode = UITextFieldViewMode.Always;
        }

        public static void MakecardView(this UIView view)
        {
            view.Layer.CornerRadius = 5;
            view.Layer.ShadowColor = UIColor.Gray.CGColor;
            view.Layer.ShadowOffset = new CGSize(width: 0, height: 1);
            view.Layer.ShadowOpacity = 0.2f;
        }

        public static void AddHearderViewShadow(this UIView view)
        {
            view.Layer.MasksToBounds = false;
            view.Layer.ShadowColor = UIColor.Gray.CGColor;
            view.Layer.ShadowOffset = new CGSize(0f, 3f);
            view.Layer.ShadowOpacity = 0.2f;
        }

        public static void MakeRoundedView(this UIView view)
        {
            view.Layer.CornerRadius = view.Frame.Height / 2;
            view.ClipsToBounds = true;
        }

        public static UIActivityIndicatorView AddActivityIndicator(this UIView view)
        {
            var activityIndicatorView = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.WhiteLarge);
            activityIndicatorView.Color = Colors.APP_BASE_COLOR.ToPlatformColor();
            activityIndicatorView.Center = view.Center;
            view.AddSubview(activityIndicatorView);
            return activityIndicatorView;
        }

        public static UIColor FromHex(this UIColor color, int hexValue)
        {
            return UIColor.FromRGB(
                (((float)((hexValue & 0xFF0000) >> 16)) / 255.0f),
                (((float)((hexValue & 0xFF00) >> 8)) / 255.0f),
                (((float)(hexValue & 0xFF)) / 255.0f)
            );
        }
    }
}
