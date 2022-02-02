using System;
using Foundation;
using UIKit;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.iOS.Views.CardList
{
    public class CardListTableSource : UITableViewSource
    {
        public CardDetailsResponse CardLists;
        public UITableView CardsTable = new UITableView();
        private VehicleInfoDisplayViewModel cardInfoViewModel;
        public CardListTableSource(VehicleInfoDisplayViewModel viewmodel)
        {
            this.cardInfoViewModel = viewmodel;
            this.CardLists = viewmodel.response;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 70;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("CardListViewCell", indexPath) as CardListViewCell;
            CardsTable = tableView;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(CardLists, indexPath);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return 1;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            CardListViewCell cell = (CardListViewCell)tableView.CellAt(indexPath);
                       
        }

    }
}
