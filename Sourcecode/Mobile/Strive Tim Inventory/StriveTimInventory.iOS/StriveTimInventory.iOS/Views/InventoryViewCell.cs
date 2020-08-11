using System;

using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class InventoryViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("InventoryViewCell");
        public static readonly UINib Nib;
        InventoryViewCell cell;
        InventoryViewModel ViewModel;

        static InventoryViewCell()
        {
            Nib = UINib.FromName("InventoryViewCell", NSBundle.MainBundle);
        }

        protected InventoryViewCell(IntPtr handle) : base(handle)
        {   
            // Note: this .ctor should not contain any initialization logic.
            this.DelayBind(() => {
                var set = this.CreateBindingSet<InventoryViewCell, ProductDetail>();
                set.Bind(ItemTitle).To(item => item.ProductName);
                set.Bind(ItemId).To(item => item.ProductId);
                set.Bind(ItemDescritption).To(item => item.ProductDescription);
                set.Bind(ItemCountLabel).To(item => item.Quantity);
                set.Apply();
            });
        }

        public void SetCell(InventoryViewCell cell,InventoryViewModel viewModel,int index)
        {
            ViewModel = viewModel;
            this.cell = cell;
            cell.ItemCountOuterView.Layer.CornerRadius = 2;
            cell.ItemCountView.Layer.BorderWidth = cell.ItemCountOuterView.Layer.BorderWidth = 1;
            cell.ItemCountView.Layer.BorderColor = cell.ItemCountOuterView.Layer.BorderColor = UIColor.Gray.CGColor;
            cell.IncrementButton.SetBackgroundImage(UIImage.FromBundle(ImageUtils.ICON_WASHER), UIControlState.Highlighted);
            cell.ItemCountLabel.Text = ViewModel.FilteredList[index].Quantity.ToString();
            cell.IncrementButton.Tag = cell.DecrementButton.Tag = index;
            cell.IncrementButton.TouchUpInside -= IncrementButtonPressed;
            cell.IncrementButton.TouchUpInside += IncrementButtonPressed;
            cell.DecrementButton.TouchUpInside -= DecrementButtonPressed;
            cell.DecrementButton.TouchUpInside += DecrementButtonPressed;
        }

        private void IncrementButtonPressed(object sender, EventArgs e)
        {
            UIButton button = (UIButton)sender;
            ViewModel.IncrementCommand((int)button.Tag);
            cell.ItemCountLabel.Text = ViewModel.FilteredList[(int)button.Tag].Quantity.ToString();
        }

        private void DecrementButtonPressed(object sender, EventArgs e)
        {
            UIButton button = (UIButton)sender;
            ViewModel.DecrementCommand((int)button.Tag);
            cell.ItemCountLabel.Text = ViewModel.FilteredList[(int)button.Tag].Quantity.ToString();
        }
    }
}
