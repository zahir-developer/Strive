using System;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public class Schedule_SelectService_Source : UITableViewSource
    {
        public bool isClicked = false;
        NSIndexPath selected_index = new NSIndexPath();
        UITableView service_tableview = new UITableView();
        ScheduleServicesViewModel viewModel;
        public Schedule_SelectService_Source(ScheduleServicesViewModel ViewModel)
        {
            this.viewModel = ViewModel;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {           
            return 150;           
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {            
            return viewModel.scheduleServices.AllServiceDetail.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {           
            service_tableview = tableView;
            var cell = tableView.DequeueReusableCell("Schedule_SelectService_Cell", indexPath) as Schedule_SelectService_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(indexPath, tableView, viewModel);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            Schedule_SelectService_Cell cell = (Schedule_SelectService_Cell)tableView.CellAt(indexPath);
            cell.updateRow(indexPath);
            foreach(var item in viewModel.scheduleServices.AllServiceDetail)
            {
                if (viewModel.scheduleServices.AllServiceDetail[indexPath.Row].ServiceName == item.ServiceName)
                {
                    CustomerScheduleInformation.ScheduleServiceID = viewModel.scheduleServices.AllServiceDetail[indexPath.Row].ServiceId;
                    CustomerScheduleInformation.ScheduleServiceType = viewModel.scheduleServices.AllServiceDetail[indexPath.Row].ServiceTypeId;
                    CustomerScheduleInformation.ScheduleServicePrice =
                        viewModel.scheduleServices.AllServiceDetail[indexPath.Row].Price;
                    CustomerScheduleInformation.ScheduleServiceName = viewModel.scheduleServices.AllServiceDetail[indexPath.Row].ServiceName;
                }
            } 
        }

        public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        {
            Schedule_SelectService_Cell cell = (Schedule_SelectService_Cell)tableView.CellAt(indexPath);
            cell.deselectRow(indexPath);
        }
    }
}
