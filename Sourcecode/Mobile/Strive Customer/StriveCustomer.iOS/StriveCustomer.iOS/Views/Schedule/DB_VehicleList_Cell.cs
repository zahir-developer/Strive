using System;

using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer.Schedule;
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
        public NSIndexPath selectedRow;
        ScheduleServicesViewModel viewModel = new ScheduleServicesViewModel();
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
            selectedRow = indexPath;
            ScheduleNow_Btn.Layer.CornerRadius = 10;
            Schedule_VhCarName.Text = dataList.Status[indexPath.Row].VehicleColor + " " + dataList.Status[indexPath.Row].VehicleMfr + " " + dataList.Status[indexPath.Row].VehicleModel ?? "";
            Schedule_VhMembership.Text = dataList.Status[indexPath.Row].MembershipName;            
        }

        partial void ScheduleNow_BtnTouch(UIButton sender)
        {
            CustomerScheduleInformation.ScheduledVehicleName = dataList.Status[selectedRow.Row].VehicleColor + " " + dataList.Status[selectedRow.Row].VehicleMfr + " " + dataList.Status[selectedRow.Row].VehicleModel ?? "";
            CustomerScheduleInformation.ScheduleSelectedVehicle = dataList.Status[selectedRow.Row];
            //var select_service = new Schedule_SelectService();
            //view.NavigationController.PushViewController(select_service, true);
            viewModel.NavToSelect_Location();
            

        }
    }
}
