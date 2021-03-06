using System;
using Foundation;
using Strive.Core.Models.Employee.CheckOut;
using UIKit;
using Xamarin.Essentials;

namespace StriveOwner.iOS.Views.CheckOut
{
    public class CheckOut_DataSource : UITableViewSource
    {
        CheckoutDetails checkoutDetails;
        CheckOutView viewController;
        public CheckOut_DataSource(CheckoutDetails checkout, CheckOutView checkOutView)
        {
            checkoutDetails = checkout;
            viewController = checkOutView;
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

        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var action1 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Hold",
                (flagAction, view, success) =>
                {
                    tableView.Editing = false;
                    HoldBtnClicked(checkoutDetails.GetCheckedInVehicleDetails.checkOutViewModel[indexPath.Row]);
                });
            action1.Image = UIImage.FromBundle("select-Contact");
            action1.BackgroundColor = ColorConverters.FromHex("#ff9d00").ToPlatformColor();

            var action2 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Complete",
                (flagAction, view, success) =>
                {
                    tableView.Editing = false;
                    CompleteBtnClicked(checkoutDetails.GetCheckedInVehicleDetails.checkOutViewModel[indexPath.Row]);
                });

            action2.Image = UIImage.FromBundle("select-Contact");
            action2.BackgroundColor = ColorConverters.FromHex("#138a32").ToPlatformColor();

            var action3 = UIContextualAction.FromContextualActionStyle(
                UIContextualActionStyle.Normal,
                "Checkout",
                (flagAction, view, success) =>
                {
                    tableView.Editing = false;
                    CheckoutBtnClicked(checkoutDetails.GetCheckedInVehicleDetails.checkOutViewModel[indexPath.Row]);
                });
            action3.Image = UIImage.FromBundle("select-Contact");
            //action3.BackgroundColor = colo(29, 201, 183);

            return UISwipeActionsConfiguration.FromActions(new UIContextualAction[] { action1, action2, action3 });
        }

        public void HoldBtnClicked(checkOutViewModel checkout)
        {
            viewController.HoldTicket(checkout);
        }

        public void CompleteBtnClicked(checkOutViewModel checkout)
        {
            viewController.CompleteTicket(checkout);
        }

        public void CheckoutBtnClicked(checkOutViewModel checkout)
        {
            viewController.CheckoutTicket(checkout);
        }
    }
}
