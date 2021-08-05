using System;
using System.Drawing;
using CoreGraphics;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class Schedule_SelectService_Cell : UITableViewCell
    {       
        NSIndexPath selected_index = new NSIndexPath();
        UITableView service_tableview = new UITableView();
        ScheduleServicesViewModel view = new ScheduleServicesViewModel();
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
            MoreValue_Const.Constant = 75;
            ViewMore_ValueLbl.Hidden = false;
            view = viewModel;

            ServiceName_Lbl.Text = viewModel.scheduleServices.AllServiceDetail[indexPath.Row].ServiceName;
            SelectService_CostLbl.Text = "$" + viewModel.scheduleServices.AllServiceDetail[indexPath.Row].Price.ToString();
            ViewMore_Btn.SetTitle("View Less", UIControlState.Normal);
        }

        partial void ViewMore_BtnTouch(UIButton sender)
        {
            if(ViewMore_Btn.TitleLabel.Text == "View Less")
            {
                serviceCell_ViewHeight.Constant = 90;
                MoreValue_Const.Constant = 0;
                ViewMore_ValueLbl.Hidden = true;
                ViewMore_Btn.SetTitle("View More", UIControlState.Normal);
            }
            else
            {
                serviceCell_ViewHeight.Constant = 190;
                MoreValue_Const.Constant = 75;
                ViewMore_ValueLbl.Hidden = false;
                ViewMore_Btn.SetTitle("View Less", UIControlState.Normal);
            }                    
        }

        public void updateRow(NSIndexPath indexPath)
        {
            SelectService_Btn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
        }

        public void deselectRow(NSIndexPath indexPath)
        {
            SelectService_Btn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
        }

        partial void SelectService_BtnTouch(UIButton sender)
        {            
            //if(SelectService_Btn.CurrentImage == UIImage.FromBundle("icon-unchecked-round"))
            //{
            //    updateRow(selected_index);
            //    var selectedItem = view.scheduleServices.AllServiceDetail[selected_index.Row];
            //    foreach (var item in view.scheduleServices.AllServiceDetail)
            //    {
            //        if (view.scheduleServices.AllServiceDetail[selected_index.Row].ServiceName == item.ServiceName)
            //        {
            //            CustomerScheduleInformation.ScheduleServiceID = view.scheduleServices.AllServiceDetail[selected_index.Row].ServiceId;
            //            CustomerScheduleInformation.ScheduleServiceType = view.scheduleServices.AllServiceDetail[selected_index.Row].ServiceTypeId;
            //            CustomerScheduleInformation.ScheduleServicePrice =
            //                view.scheduleServices.AllServiceDetail[selected_index.Row].Price;
            //            CustomerScheduleInformation.ScheduleServiceName = view.scheduleServices.AllServiceDetail[selected_index.Row].ServiceName;
            //        }
            //    }
            //}
            //else
            //{
            //    deselectRow(selected_index);
            //}            
        }
    }
}
