using System;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public class ScheduleLocation_Source : UITableViewSource
    {
        ScheduleLocationsViewModel ViewModel;
        public ScheduleLocation_Source(ScheduleLocationsViewModel viewModel)
        {
            this.ViewModel = viewModel;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 45;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ViewModel.Locations.Location.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("Schedule_Location_Cell", indexPath) as Schedule_Location_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(ViewModel, indexPath);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            Schedule_Location_Cell cell = (Schedule_Location_Cell)tableView.CellAt(indexPath);
            cell.updateCell(indexPath);
            
            CustomerScheduleInformation.ScheduleLocationCode = ViewModel.Locations.Location[indexPath.Row].LocationId;
            CustomerScheduleInformation.ScheduleLocationAddress = ViewModel.Locations.Location[indexPath.Row].Address1;
        }

        public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        {
            Schedule_Location_Cell cell = (Schedule_Location_Cell)tableView.CellAt(indexPath);
            cell.deselectRow(indexPath);
        }

        

    }
}