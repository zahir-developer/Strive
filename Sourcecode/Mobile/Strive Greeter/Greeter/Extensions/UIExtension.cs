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

    }
}
