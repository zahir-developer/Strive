using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;
using System.Collections.Generic;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public class SelectMembershipSource : MvxTableViewSource
    {

        private static string CellId = "ClientTableViewCell";

        private SelectMembershipViewModel ViewModel;

        private ObservableCollection<MembershipServices> ItemList;

        ClientTableViewCell firstselected = null;
        ClientTableViewCell secondselected = null;
        int Selectedindex = 0;

        List<ClientTableViewCell> CellList = new List<ClientTableViewCell>();

        public SelectMembershipSource(UITableView tableView, SelectMembershipViewModel ViewModel) : base(tableView)
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
                    ItemList = (ObservableCollection<MembershipServices>)value;
                }
                else
                {
                    ItemList = new ObservableCollection<MembershipServices>();
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
            ClientTableViewCell.SelectedCell.DeSelectMembershipcell();
            var cell = (ClientTableViewCell)tableView.CellAt(indexPath);
            Selectedindex = indexPath.Row;
            if (firstselected == null)
            {
                firstselected = cell;
                firstselected.SelectMembershipcell();
                MembershipData.SelectedMembership = ItemList[Selectedindex];
            }
            else
            {
                secondselected = cell;
                if(firstselected == secondselected)
                {
                    firstselected.DeSelectMembershipcell();
                    firstselected = secondselected = null;
                    MembershipData.SelectedMembership = null;
                }
                else
                {
                    firstselected.DeSelectMembershipcell();
                    secondselected.SelectMembershipcell();
                    firstselected = secondselected;
                    
                    secondselected = null;
                    MembershipData.SelectedMembership = ItemList[Selectedindex];
                }
            }
        }


        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            ClientTableViewCell cell = (ClientTableViewCell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetMembershipList(ItemList[indexPath.Row],indexPath.Row,Selectedindex,cell);
            return cell;
        }
    }
}
