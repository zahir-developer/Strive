using System;

using Foundation;
using Strive.Core.Models.TimInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class ClientTableViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ClientTableViewCell`");
        public static readonly UINib Nib;

        static ClientTableViewCell()
        {
            Nib = UINib.FromName("ClientTableViewCell", NSBundle.MainBundle);
        }

        protected ClientTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetMembershipList(string item)
        {
            ClientName.Text = item;
        }

        public void SetClientDetail(ClientDetail item)
        {
            ClientName.Text = item.FirstName;
        }

        public void SelectMembershipcell()
        {
            ClientName.TextColor = UIColor.Cyan;
        }

        public void DeSelectMembershipcell()
        {
            ClientName.TextColor = UIColor.Black;
        }

        public void SetUpchargeList(string item)
        {
            ClientName.Text = item;
        }

        public void SetExtraServiceList(string item)
        {
            ClientName.Text = item;
        }
    }
}
