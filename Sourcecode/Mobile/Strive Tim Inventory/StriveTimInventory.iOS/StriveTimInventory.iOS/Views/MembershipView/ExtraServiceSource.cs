using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public class ExtraServiceSource : MvxTableViewSource
    {

        private static string CellId = "ClientTableViewCell";

        private ExtraServiceViewModel ViewModel;

        private ObservableCollection<ServiceDetail> ItemList;

        private ObservableCollection<ServiceDetail> ExtraList = new ObservableCollection<ServiceDetail>();

        private ObservableCollection<ServiceDetail> MembershipServiceList = new ObservableCollection<ServiceDetail>();

        ClientTableViewCell firstselected = null;
        ClientTableViewCell secondselected = null;

        public ExtraServiceSource(UITableView tableView, ExtraServiceViewModel ViewModel) : base(tableView)
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
                    ItemList = (ObservableCollection<ServiceDetail>)value;
                    MembershipServiceList = ViewModel.SelectedServiceList;
                }
                else
                {
                    ItemList = new ObservableCollection<ServiceDetail>();
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
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {

            var cell = (ClientTableViewCell)tableView.CellAt(indexPath);

            if (ExtraList.Contains(ItemList[indexPath.Row]))
            {
                ExtraList.Remove(ItemList[indexPath.Row]);
                cell.DeSelectMembershipcell();
            }
            else
            {
                ExtraList.Add(ItemList[indexPath.Row]);
                cell.SelectMembershipcell();
            }
        }


        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            ClientTableViewCell cell = (ClientTableViewCell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetExtraServiceList(ItemList[indexPath.Row], ExtraList,MembershipServiceList,cell);
            return cell;
        }
    }
}
