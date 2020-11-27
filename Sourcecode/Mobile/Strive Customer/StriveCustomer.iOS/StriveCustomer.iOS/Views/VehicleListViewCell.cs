using System;

using Foundation;
using ObjCRuntime;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using UIKit;

namespace StriveCustomer.iOS.Views
{    
    public partial class VehicleListViewCell : UITableViewCell
    {
        ProfileView view = new ProfileView();
        public static readonly NSString Key = new NSString("VehicleListViewCell");
        public static readonly UINib Nib;
        public int selectedRow;

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
            selectedRow = indexpath.Row;
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

        partial void DeleteVehicleList_BtnTouch(UIButton sender)
        {            
            CustomerInfo.actionType = 1;
            view.selectedVehicle(selectedRow);
        }

        partial void EditVehicleList_BtnTouch(UIButton sender)
        {
            CustomerInfo.actionType = 2;            
        }
    }
}
