using System;
using System.Globalization;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class DealsViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("DealsViewCell");
        public static readonly UINib Nib;

        static DealsViewCell()
        {
            Nib = UINib.FromName("DealsViewCell", NSBundle.MainBundle);
        }

        protected DealsViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.          
        }

        public void SetCell(GetAllDeal item)
        {
            BackgroundView.Layer.CornerRadius = 5;
            TitleLabel.Text = item.DealName;

            var date1 = item.StartDate;
            var FullSplitDates = date1.Split("-");
            var fullDateInfo = FullSplitDates[0].Substring(0, 4);
            var month1 = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(FullSplitDates[1]));
            var shortMon1 = month1.Substring(0, 3);

            var date2 = item.EndDate;
            var FullSplitDates2 = date2.Split("-");
            var fullDateInfo2 = FullSplitDates[0].Substring(0, 4);
            var month2 = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(FullSplitDates2[1]));
            var shortMon2 = month2.Substring(0, 3);

            if((int.Parse(fullDateInfo) < 2000) && (int.Parse(fullDateInfo2) < 2000))
            {
                ValidityLabel.Text = "Validity: None";
            }
            else
            {
                ValidityLabel.Text = "Validity: " + FullSplitDates[2].Substring(0, 2) + " " + shortMon1 + " " + fullDateInfo + " to " + FullSplitDates2[2].Substring(0, 2) + " " + shortMon2 + " " + fullDateInfo2;
            }
        }
    }
}
