using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public class InventoryTableViewDataSource : MvxTableViewSource
    {

        private static string CellId = "InventoryViewCell";

        private InventoryViewModel ViewModel;

        private ObservableCollection<ProductDetail> ItemList;

        public InventoryTableViewDataSource(UITableView tableView, InventoryViewModel ViewModel) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(InventoryViewCell.Nib, CellId);
            this.ViewModel = ViewModel;
        }

        public override IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                if (value != null)
                {
                    ItemList = (ObservableCollection<ProductDetail>)value;
                }
                else
                {
                    ItemList = new ObservableCollection<ProductDetail>();
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
            return ItemList[indexPath.Row].DisplayRequestView
                ? InventoryViewCell.ExpandedHeight
                : InventoryViewCell.NormalHeight; 
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
            InventoryViewCell cell = (InventoryViewCell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetCell(cell,ViewModel,indexPath.Row);
            cell.SetupCell(ItemList[indexPath.Row], () =>
               tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.None));
            return cell;
        }
    }
}
