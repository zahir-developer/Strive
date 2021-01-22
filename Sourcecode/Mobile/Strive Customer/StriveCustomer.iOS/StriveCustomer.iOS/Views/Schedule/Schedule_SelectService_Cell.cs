using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using Strive.Core.ViewModels.Customer.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_SelectService_Cell : UITableViewCell
    {
        //Schedule_SelectService_Source source = new Schedule_SelectService_Source();
        NSIndexPath selected_index = new NSIndexPath();
        UITableView service_tableview = new UITableView();
        public static readonly NSString Key = new NSString("Schedule_SelectService_Cell");
        public static readonly UINib Nib;

        static Schedule_SelectService_Cell()
        {
            Nib = UINib.FromName("Schedule_SelectService_Cell", NSBundle.MainBundle);
        }

        protected Schedule_SelectService_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(NSIndexPath indexPath, UITableView tableView, ScheduleServicesViewModel viewModel)
        {
            selected_index = indexPath;
            service_tableview = tableView;
            SelectService_CellView.Layer.CornerRadius = 5;            
            MoreValue_Const.Constant = 0;
            ViewMore_ValueLbl.Hidden = true;

            ServiceName_Lbl.Text = viewModel.scheduleServices.ServicesWithPrice[indexPath.Row].ServiceName;
            SelectService_CostLbl.Text = "$" + viewModel.scheduleServices.ServicesWithPrice[indexPath.Row].Price.ToString();
        }

        partial void ViewMore_BtnTouch(UIButton sender)
        {                
            serviceCell_ViewHeight.Constant = 190;
            MoreValue_Const.Constant = 75;
            ViewMore_ValueLbl.Hidden = false;
            ViewMore_Btn.SetTitle("View Less", UIControlState.Normal);            
        }       
    }
}
