using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class InventoryViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("InventoryViewCell");
        public static readonly UINib Nib;

        static InventoryViewCell()
        {
            Nib = UINib.FromName("InventoryViewCell", NSBundle.MainBundle);
        }

        protected InventoryViewCell(IntPtr handle) : base(handle)
        {
            
            
            // Note: this .ctor should not contain any initialization logic.
            this.DelayBind(() => {
                var set = this.CreateBindingSet<InventoryViewCell, string>();
                set.Bind(ItemTitle).To(item => item);
                set.Apply();
            });

        }

        public void SetCell(InventoryViewCell cell)
        {
            cell.ItemCountOuterView.Layer.CornerRadius = 2;
            cell.ItemCountView.Layer.BorderWidth = cell.ItemCountOuterView.Layer.BorderWidth = 1;
            cell.ItemCountView.Layer.BorderColor = cell.ItemCountOuterView.Layer.BorderColor = UIColor.Gray.CGColor;
        }
    }
}
