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
            service_tableview = tableView;
            SelectService_CellView.Layer.CornerRadius = 5;            
            MoreValue_Const.Constant = 75;
            ViewMore_ValueLbl.Hidden = false;
            view = viewModel;

            ServiceName_Lbl.Text = viewModel.scheduleServices.AllServiceDetail[indexPath.Row].ServiceName;
            SelectService_CostLbl.Text = "$" + viewModel.scheduleServices.AllServiceDetail[indexPath.Row].Price.ToString();
            ViewMore_Btn.SetTitle("View Less", UIControlState.Normal);
            SelectService_Btn.Tag = indexPath.Row;
            if(selected_index == indexPath)
                SelectService_Btn.Image = UIImage.FromBundle("icon-checked-round");
            else
                SelectService_Btn.Image = UIImage.FromBundle("icon-unchecked-round");
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
            if(SelectService_Btn.Tag == indexPath.Row)
            {
                SelectService_Btn.Image = UIImage.FromBundle("icon-checked-round");
                selected_index = indexPath;
            }
            else
            {
                SelectService_Btn.Image = UIImage.FromBundle("icon-unchecked-round");
            }
        }

        public void deselectRow(NSIndexPath indexPath)
        {
            SelectService_Btn.Image = UIImage.FromBundle("icon-unchecked-round");
            selected_index = null;
        }

        //partial void SelectService_BtnTouch(UIButton sender)
        //{
        //    if (SelectService_Btn.CurrentImage == UIImage.FromBundle("icon-unchecked-round"))
        //    {
        //        SelectService_Btn.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
        //        var selectedItem = view.scheduleServices.AllServiceDetail[int.Parse(sender.Tag.ToString())];
        //        foreach (var item in view.scheduleServices.AllServiceDetail)
        //        {
        //            if (view.scheduleServices.AllServiceDetail[int.Parse(sender.Tag.ToString())].ServiceName == item.ServiceName)
        //            {
        //                CustomerScheduleInformation.ScheduleServiceID = view.scheduleServices.AllServiceDetail[int.Parse(sender.Tag.ToString())].ServiceId;
        //                CustomerScheduleInformation.ScheduleServiceType = view.scheduleServices.AllServiceDetail[int.Parse(sender.Tag.ToString())].ServiceTypeId;
        //                CustomerScheduleInformation.ScheduleServicePrice =
        //                    view.scheduleServices.AllServiceDetail[int.Parse(sender.Tag.ToString())].Price;
        //                CustomerScheduleInformation.ScheduleServiceName = view.scheduleServices.AllServiceDetail[int.Parse(sender.Tag.ToString())].ServiceName;
        //                CustomerScheduleInformation.ScheduleServiceEstimatedTime = view.scheduleServices.AllServiceDetail[int.Parse(sender.Tag.ToString())].EstimatedTime ?? 0;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        SelectService_Btn.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
        //    }                      
        //}
    }
}
