using System;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.iOS.Views.Schedule;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class ScheduleVehcileListSource : UITableViewSource
    {
        public VehicleList scheduleVehicleList;
        public MvxViewController view;
        public ScheduleVehcileListSource(MvxViewController scheduleView ,ScheduleViewModel viewModel)
        {
            this.view = scheduleView;
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
            cell.SetData(scheduleVehicleList, indexPath, view);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            CustomerScheduleInformation.ScheduledVehicleName = scheduleVehicleList.Status[indexPath.Row].VehicleColor +
                " " + scheduleVehicleList.Status[indexPath.Row].VehicleMfr;
            CustomerScheduleInformation.ScheduleSelectedVehicle = scheduleVehicleList.Status[indexPath.Row];
            var select_service = new Schedule_SelectService();
            view.NavigationController.PushViewController(select_service, true);
        }
    }
}
