using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class DealsTableSource : MvxTableViewSource
    {

        private static string CellId = "DealsViewCell";

        private DealsViewModel ViewModel;

        public DealsPageViewModel dealsPage;

        private ObservableCollection<GetAllDeal> ItemList;

        public DealsTableSource(UITableView tableView, DealsViewModel ViewModel) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(DealsViewCell.Nib, CellId);
            this.ViewModel = ViewModel;
        }

        public override IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                if (value != null)
                {
                    ItemList = (ObservableCollection<GetAllDeal>)value;
                }
                else
                {
                    ItemList = new ObservableCollection<GetAllDeal>();
                }

                base.ItemsSource = value;
            }
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            var itm = ItemList[indexPath.Row];
            return itm;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 95;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ItemList.Count();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var item = ItemList[indexPath.Row];
            var cell = GetOrCreateCellFor(tableView, indexPath, item);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            IMvxDataConsumer bindable = cell as IMvxDataConsumer;
            if (bindable != null)
            {
                bindable.DataContext = item;
            }
            return cell;
        }


        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            DealsViewCell cell = (DealsViewCell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetCell(ItemList[indexPath.Row]);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            DealsViewCell cell = (DealsViewCell)tableView.CellAt(indexPath);
            DealsViewModel.SelectedDealId = ViewModel.Deals[indexPath.Row].DealId;
            ViewModel.NavigateToDealsPageCommand();
        }
    }

}
