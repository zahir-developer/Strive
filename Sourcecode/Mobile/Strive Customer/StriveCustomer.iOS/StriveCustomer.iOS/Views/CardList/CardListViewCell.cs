using System;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views.CardList
{
    public partial class CardListViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CardListViewCell");
        public static readonly UINib Nib;
        public NSIndexPath selectedRow;
        public CardDetailsResponse dataList;
        public string cardno;
        private VehicleInfoDisplayViewModel CardViewModel = new VehicleInfoDisplayViewModel();

        static CardListViewCell()
        {
            Nib = UINib.FromName("CardListViewCell", NSBundle.MainBundle);
        }

        protected CardListViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(CardDetailsResponse list, NSIndexPath indexpath)
        {
            dataList = list;
            CardListView.Layer.CornerRadius = 5;
            selectedRow = indexpath;
            //EditVehicleBtn.SetImage(UIImage.FromBundle("icon-edit-personalInfo"), UIControlState.Normal);
            //deleteVehicleBtn.SetImage(UIImage.FromBundle("icon-delete"), UIControlState.Normal);
            cardno = list.CardNumber;
            CardNumber.Text = cardno;
            //CardNumber.Text = "xxxxxxxxxxxx8393";
            ExpiryDate.Text = list.ExpiryDate;
            //ExpiryDate.Text = "09/23";
        }
 
    }
}

