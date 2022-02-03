
using System;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;

namespace StriveTimInventory.iOS.Views.CardList
{
    public partial class CardListViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CardListViewCell");
        public static readonly UINib Nib;
        public NSIndexPath selectedRow;
        public CardDetailsResponse dataList;
        public string cardno;
        private VehicleMembershipDetailViewModel CardViewModel = new VehicleMembershipDetailViewModel();
        //

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
            cardno = list.CardNumber;
            CardNumber.Text = "xxxxxxxxxxxx" + cardno.Substring(11, 4);
            //CardNumber.Text = "xxxxxxxxxxxx8393";
            ExpiryDate.Text = list.ExpiryDate;
            //ExpiryDate.Text = "09/23";
        }
        
    }
}

