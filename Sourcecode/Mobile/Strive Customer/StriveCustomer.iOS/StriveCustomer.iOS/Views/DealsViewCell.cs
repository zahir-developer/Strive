using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
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
            this.DelayBind(() =>
            {
                var set = (this).CreateBindingSet<DealsViewCell, string>();
                set.Bind(TitleLabel).To(item => item);
                set.Apply();
            });

        }

        public void SetCell()
        {
            BackgroundView.Layer.CornerRadius = 5;
        }
    }
}
