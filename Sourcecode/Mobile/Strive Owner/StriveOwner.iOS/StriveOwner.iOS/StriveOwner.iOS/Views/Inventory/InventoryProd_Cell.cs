using System;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    public partial class InventoryProd_Cell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("InventoryProd_Cell");
        public static readonly UINib Nib;
        InventoryProd_Cell cell;
        InventoryViewModel ViewModel;

        private InventoryDataModel model;
        private Action reloadParentRow { get; set; }

        public static readonly int ExpandedHeight = 380;
        public static readonly int NormalHeight = 120;
        public int quantity;
        public int index;
        UIAlertView alert = new UIAlertView();

        static InventoryProd_Cell()
        {
            Nib = UINib.FromName("InventoryProd_Cell", NSBundle.MainBundle);
        }

        protected InventoryProd_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            this.DelayBind(() => {
                var set = this.CreateBindingSet<InventoryProd_Cell, InventoryDataModel>();
                set.Bind(ItemTitle).To(item => item.Product.ProductName);
                set.Bind(ItemId).To(item => item.Product.ProductCode);
                set.Bind(ItemDescription).To(item => item.Product.ProductDescription);
                set.Bind(ItemCountLabel).To(item => item.Product.Quantity);
                set.Bind(SupplierName).To(item => item.Vendor.VendorName);
                set.Bind(SupplierContact).To(item => item.Vendor.PhoneNumber);
                //set.Bind(SupplierFax).To(item => item.Vendor.Fax);
                set.Bind(SupplierAddress).To(item => item.Vendor.Address1);
                set.Bind(SupplierEmail).To(item => item.Vendor.Email);
                set.Apply();
            });
        }

        public void SetCell(InventoryProd_Cell cell, InventoryViewModel viewModel, int index)
        {
            ViewModel = viewModel;
            this.cell = cell;
            cell.ItemCountOuterView.Layer.CornerRadius = 2;
            cell.ItemCountView.Layer.BorderWidth = cell.ItemCountOuterView.Layer.BorderWidth = 1;
            cell.ItemCountView.Layer.BorderColor = cell.ItemCountOuterView.Layer.BorderColor = UIColor.Gray.CGColor;
            //cell.IncrementButton.SetBackgroundImage(UIImage.FromBundle(ImageUtils.ICON_WASHER), UIControlState.Highlighted);
            cell.ItemCountLabel.Text = ViewModel.FilteredList[index].Product.Quantity.ToString();
            cell.RequestView.Hidden = false;
            cell.RequestButton.Layer.CornerRadius = 5;
            cell.IncrementButton.Tag = cell.DecrementButton.Tag = cell.ItemEditButton.Tag = cell.RequestButton.Tag = index;

            if (ViewModel.FilteredList[index].Product.Base64 != null)
            {
                var imageBytes = Convert.FromBase64String(ViewModel.FilteredList[index].Product.Base64);
                var imageData = NSData.FromArray(imageBytes);
                var uiImage = UIImage.LoadFromData(imageData);
                cell.ItemImage.Image = uiImage;
            }
            else
            {
                cell.ItemImage.Image = UIImage.FromBundle("white-ellipse");
            }

            cell.IncrementButton.TouchUpInside -= IncrementButtonPressed;
            cell.IncrementButton.TouchUpInside += IncrementButtonPressed;
            cell.DecrementButton.TouchUpInside -= DecrementButtonPressed;
            cell.DecrementButton.TouchUpInside += DecrementButtonPressed;
            cell.ViewMoreButton.TouchUpInside -= ShowMoreButtonPressed;
            cell.ViewMoreButton.TouchUpInside += ShowMoreButtonPressed;
        }

        private void IncrementButtonPressed(object sender, EventArgs e)
        {
            UIButton button = (UIButton)sender;
            ViewModel.IncrementCommand((int)button.Tag);
            cell.ItemCountLabel.Text = ViewModel.FilteredList[(int)button.Tag].Product.Quantity.ToString();
        }

        private void DecrementButtonPressed(object sender, EventArgs e)
        {
            UIButton button = (UIButton)sender;
            ViewModel.DecrementCommand((int)button.Tag);
            cell.ItemCountLabel.Text = ViewModel.FilteredList[(int)button.Tag].Product.Quantity.ToString();
        }

        private void ShowMoreButtonPressed(object sender, EventArgs e)
        {

            if (SupplierHeightConstraint.Constant == 0)
            {
                ShowRequestView();
                model.DisplayRequestView = true;
            }
            else
            {
                HideRequestView();
                model.DisplayRequestView = false;
            }
            LayoutSubviews();
            reloadParentRow();
        }

        public void SetupCell(InventoryDataModel model, Action reloadParentRow)
        {
            this.model = model;
            this.reloadParentRow = reloadParentRow;

            if (model.DisplayRequestView)
            {
                ShowRequestView();
            }
            else
            {
                HideRequestView();
            }
        }

        public void HideRequestView()
        {
            cell.ViewMoreButton.SetTitle("View More", UIControlState.Normal);
            SupplierHeightConstraint.Constant = 0;
            cell.RequestView.Hidden = true;
        }

        public void ShowRequestView()
        {
            cell.ViewMoreButton.SetTitle("View less", UIControlState.Normal);
            SupplierHeightConstraint.Constant = 260;
            cell.RequestView.Hidden = false;
        }

        partial void EditItemTouch(UIButton sender)
        {
            ViewModel.EditCommand((int)sender.Tag);
        }

        partial void RequestBtnTouch(UIButton sender)
        {
            index = (int)sender.Tag;

            alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
            alert.Title = "Product Request";
            alert.GetTextField(0).Placeholder = "Enter the quantity";
            alert.AddButton("OK");
            alert.Show();

            alert.Clicked += Alert_Clicked;
        }

        private void Alert_Clicked(object sender, UIButtonEventArgs e)
        {
            string request = alert.GetTextField(0).Text;
            if (request != "")
            {
                quantity = int.Parse(request);
            }
            if (quantity > 0)
            {
                ViewModel.ProductRequestCommand(quantity, index);
            }
        }
    }
}
