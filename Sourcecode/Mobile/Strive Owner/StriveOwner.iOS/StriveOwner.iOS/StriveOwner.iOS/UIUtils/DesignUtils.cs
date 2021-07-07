using System;
using UIKit;

namespace StriveOwner.iOS.UIUtils
{
    public static class DesignUtils
    {
        public static UIFont OpenSansExtraBold()
        {
            return UIFont.FromName("OpenSans-ExtraBold", 25f);
        }

        public static UIFont OpenSansBoldEighteen()
        {
            return UIFont.FromName("OpenSans-Bold", 18f);
        }

        public static UIFont OpenSansBoldFifteen()
        {
            return UIFont.FromName("OpenSans-Bold", 15f);
        }

        public static UIFont OpenSansRegularTwelve()
        {
            return UIFont.FromName("OpenSans-Regular", 12f);
        }

        public static UIFont OpenSansExtraBoldBig()
        {
            return UIFont.FromName("OpenSans-ExtraBold", 45f);
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
