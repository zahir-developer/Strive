using System;
using MaterialComponents;
using UIKit;
using Xamarin.Essentials;

namespace Greeter.CustomView
{
    public class FloatingTextField2 : UIView
    {
        TextInputControllerUnderline textInputControllerOutlined;

        public FloatingTextField2(IntPtr p)
          : base(p)
        {
            Initialize();
        }

        public FloatingTextField2()
        {
            Initialize();
        }

        void Initialize()
        {
            var t = new TextField();
            t.AutocorrectionType = UITextAutocorrectionType.No;
            t.ClearButtonMode = UITextFieldViewMode.Never;
            t.Frame = new CoreGraphics.CGRect(this.Frame.X, this.Frame.Y, this.Frame.Height, this.Frame.Width);

            textInputControllerOutlined = new TextInputControllerUnderline(t);
            textInputControllerOutlined.PlaceholderText = "Test Placeholder";
            textInputControllerOutlined.ActiveColor = ColorConverters.FromHex("#4A90E2").ToPlatformColor();
            textInputControllerOutlined.FloatingPlaceholderActiveColor = ColorConverters.FromHex("#4A90E2").ToPlatformColor();
            //SetNeedsDisplay();
        }

        //string placeholderText;
        //[Export("PlaceholderText"), Browsable(true)]
        //public string PlaceholderText
        //{
        //    get
        //    {
        //        return placeholderText;
        //    }
        //    set
        //    {
        //        //textInputControllerOutlined.PlaceholderText = value;
        //        //SetNeedsDisplay();

        //        placeholderText = value;
        //        SetNeedsDisplay();
        //    }
        //}
    }
}
