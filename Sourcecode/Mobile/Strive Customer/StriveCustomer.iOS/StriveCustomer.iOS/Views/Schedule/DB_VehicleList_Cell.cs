using System;

using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using StriveCustomer.iOS.Views.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class DB_VehicleList_Cell : UITableViewCell
    {        
        public static readonly NSString Key = new NSString("DB_VehicleList_Cell");
        public static readonly UINib Nib;
        public VehicleList dataList;
        public MvxViewController view;

        static DB_VehicleList_Cell()
        {
            Nib = UINib.FromName("DB_VehicleList_Cell", NSBundle.MainBundle);
        }

        protected DB_VehicleList_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(VehicleList list, NSIndexPath indexPath, MvxViewController viewController)
        {
            view = viewController;
            dataList = list;
            ScheduleNow_Btn.Layer.CornerRadius = 10;
            Schedule_VhCarName.Text = dataList.Status[indexPath.Row].VehicleColor + " " + dataList.Status[indexPath.Row].VehicleMfr + " " + dataList.Status[indexPath.Row].VehicleModel ?? "";

            if (dataList.Status[indexPath.Row].IsMembership)
            {
                Schedule_VhMembership.Text = "Yes";
            }
            else
            {
                Schedule_VhMembership.Text = "No";
            }
        }

        partial void ScheduleNow_BtnTouch(UIButton sender)
        {
            //CustomerScheduleInformation.ScheduledVehicleName = dataList.Status[indexPath.Row].VehicleColor + " " + dataList.Status[indexPath.Row].VehicleMfr;
            var select_service = new Schedule_SelectService();
            view.NavigationController.PushViewController(select_service, true);
        }
    }
}
