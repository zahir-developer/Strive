using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class PastDetailTableSource : MvxTableViewSource
    {
        private static string CellId = "PastDetailViewCell";

        private LoginViewModel ViewModel;

        private ObservableCollection<string> ItemList = new ObservableCollection<String>();

        public PastDetailTableSource(UITableView tableView, LoginViewModel ViewModel) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(PastDetailViewCell.Nib, CellId);
            this.ViewModel = ViewModel;

            ItemList.Add("Rose");
            ItemList.Add("Jasmine");
            ItemList.Add("Lotus");
            ItemList.Add("Lily");
            ItemList.Add("Hibiscus");
            ItemList.Add("Daisy");
        }

        public override IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                if (value != null)
                {
                    ItemList = (ObservableCollection<string>)value;
                }
                else
                {
                    ItemList = new ObservableCollection<string>();
                }

                base.ItemsSource = value;
            }
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            var item = ItemList[indexPath.Row];
            return item;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 50;
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
            PastDetailViewCell cell = (PastDetailViewCell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetCell();
            return cell;
        }
    }
}
