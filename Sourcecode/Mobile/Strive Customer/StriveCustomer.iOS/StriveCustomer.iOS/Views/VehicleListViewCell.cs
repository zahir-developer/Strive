using System;

using Foundation;
using Strive.Core.Models.TimInventory;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class VehicleListViewCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("VehicleListViewCell");
        public static readonly UINib Nib;

        static VehicleListViewCell()
        {
            Nib = UINib.FromName("VehicleListViewCell", NSBundle.MainBundle);
        }

        protected VehicleListViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(VehicleList list, NSIndexPath indexpath)
        {
            VehicleList_CellView.Layer.CornerRadius = 5;
            EditVehicleBtn.SetImage(UIImage.FromBundle("icon-edit-personalInfo"), UIControlState.Normal);
            deleteVehicleBtn.SetImage(UIImage.FromBundle("icon-delete"), UIControlState.Normal);

            VehicleList_CarNameLabel.Text = list.Status[indexpath.Row].VehicleColor + " " + list.Status[indexpath.Row].VehicleMfr + " " + list.Status[indexpath.Row].VehicleModel ?? "";
            VehicleList_RegNoLabel.Text = list.Status[indexpath.Row].VehicleNumber ?? "";

            if (list.Status[indexpath.Row].IsMembership)
            {
                VehicleList_MembershipLabel.Text = "Yes";
            }
            else
            {
                VehicleList_MembershipLabel.Text = "No";
            }
        }
    }
}
