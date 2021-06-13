using System;
using UIKit;

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
            view.Layer.ShadowColor = UIColor.LightGray.CGColor;
            view.Layer.ShadowOffset = new CoreGraphics.CGSize(width: 0, height: 0);
            view.Layer.ShadowRadius = 6;
            view.Layer.ShadowOpacity = 0.3f;
        }

    }
}
