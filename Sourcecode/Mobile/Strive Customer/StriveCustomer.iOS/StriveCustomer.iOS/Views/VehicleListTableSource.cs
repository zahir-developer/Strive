using System;
using Foundation;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class VehicleListTableSource : UITableViewSource
    {
        private VehicleList vehicleLists;
        public VehicleListTableSource(VehicleList data)
        {
            this.vehicleLists = data;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 80;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return vehicleLists.Status.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("VehicleListViewCell", indexPath) as VehicleListViewCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(vehicleLists, indexPath);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            VehicleListViewCell cell = (VehicleListViewCell)tableView.CellAt(indexPath);
            //CustomerInfo.SelectedVehiclePastDetails = services.PastClientDetails[indexPath.Row].VehicleId;
            //var pastTabView = new PastDetailTabView();
            //view.NavigationController.PushViewController(pastTabView, true);
        }
    }

}