using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public class ClientTableSource : MvxTableViewSource
    {

        private static string CellId = "ClientTableViewCell";

        private MembershipClientListViewModel ViewModel;

        //private List<ClientInfo> ItemList;
        private ObservableCollection<ClientViewModel> ItemList;

        public ClientTableSource(UITableView tableView, MembershipClientListViewModel ViewModel) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(ClientTableViewCell.Nib, CellId);
            this.ViewModel = ViewModel;
        }

        public override IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                if (value != null)
                {
                    ItemList = (ObservableCollection<ClientViewModel>)value;
                }
                else
                {
                    ItemList = new ObservableCollection<ClientViewModel>();
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
            return 80;
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
            ClientTableViewCell cell = (ClientTableViewCell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetClientDetail(ItemList[indexPath.Row]);
            return cell;
        }
    }
}
