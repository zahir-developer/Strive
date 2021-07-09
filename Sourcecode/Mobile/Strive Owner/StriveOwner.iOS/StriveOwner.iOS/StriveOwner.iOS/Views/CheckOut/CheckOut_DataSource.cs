using System;
using Foundation;
using Strive.Core.Models.Employee.CheckOut;
using UIKit;

namespace StriveOwner.iOS.Views.CheckOut
{
    public class CheckOut_DataSource : UITableViewSource
    {
        CheckoutDetails checkoutDetails;
        public CheckOut_DataSource(CheckoutDetails checkout)
        {
            checkoutDetails = checkout;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }        

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return checkoutDetails.GetCheckedInVehicleDetails.checkOutViewModel.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("CheckOut_Cell", indexPath) as CheckOut_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetupData(checkoutDetails.GetCheckedInVehicleDetails.checkOutViewModel[indexPath.Row]);
            return cell;
        }
    }
}
