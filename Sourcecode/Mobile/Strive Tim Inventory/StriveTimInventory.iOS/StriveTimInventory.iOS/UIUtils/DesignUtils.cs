using System;
using UIKit;

namespace StriveTimInventory.iOS.UIUtils
{
    public static class DesignUtils
    {
        public static UIFont OpenSansBoldTitle()
        {
            return UIFont.FromName("OpenSans-Bold", 22f);
        }

        public static UIFont OpenSansBoldBig()
        {
            return UIFont.FromName("OpenSans-Bold", 24f);
        }

        public static UIFont OpenSansBoldButton()
        {
            return UIFont.FromName("OpenSans-Bold", 20f);
        }

        public static UIFont OpenSansRegularText()
        {
            return UIFont.FromName("OpenSans-Regular", 17f);
        }
    }

    public static class UIColorExtensions
    {
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
