using System;
using Foundation;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class ScheduleVehcileListSource : UITableViewSource
    {
        public VehicleList scheduleVehicleList;
        public ScheduleVehcileListSource(ScheduleViewModel viewModel)
        {
            this.scheduleVehicleList = viewModel.scheduleVehicleList;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 90;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return scheduleVehicleList.Status.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DB_VehicleList_Cell", indexPath) as DB_VehicleList_Cell;            
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(scheduleVehicleList, indexPath);
            return cell;
        }
    }
}
