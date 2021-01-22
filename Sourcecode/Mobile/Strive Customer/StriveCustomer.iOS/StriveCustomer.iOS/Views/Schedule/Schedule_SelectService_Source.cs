using System;
using Foundation;
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
            if (isClicked)
            {
                return 200;
                tableView.ReloadInputViews();
            }
            else
            {
                return 90;
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 10;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            selected_index = indexPath;
            service_tableview = tableView;
            var cell = tableView.DequeueReusableCell("Schedule_SelectService_Cell", indexPath) as Schedule_SelectService_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(indexPath, tableView, viewModel);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            isClicked = true;
            GetHeightForRow(tableView, indexPath);
        }        
    }
}
