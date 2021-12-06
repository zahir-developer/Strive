using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public class SelectUpchargeSource : MvxTableViewSource
    {

        private static string CellId = "ClientTableViewCell";

        private SelectUpchargeViewModel ViewModel;

        private ObservableCollection<string> ItemList;

        ClientTableViewCell firstselected = null;
        ClientTableViewCell secondselected = null;

        public SelectUpchargeSource(UITableView tableView, SelectUpchargeViewModel ViewModel) : base(tableView)
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
                    
                    ItemList = (ObservableCollection<string>)value;
                    ItemList.Insert(0, "None");

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
            var itm = ItemList[indexPath.Row];
            return itm;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 80;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
           // ItemList.Add("None");
            return ItemList.Count();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var item = ItemList[indexPath.Row];
            var cell = GetOrCreateCellFor(tableView, indexPath, item);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            return cell;
        }

        //public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        //{

        //    var cell = (ClientTableViewCell)tableView.CellAt(indexPath);
        //    if (firstselected == null)
        //    {
        //        firstselected = cell;
        //        firstselected.SelectMembershipcell();
        //    }
        //    else
        //    {
        //        secondselected = cell;
        //        if (firstselected == secondselected)
        //        {
        //            firstselected.DeSelectMembershipcell();
        //            firstselected = secondselected = null;
        //        }
        //        else
        //        {
        //            firstselected.DeSelectMembershipcell();
        //            secondselected.SelectMembershipcell();
        //            firstselected = secondselected;
        //            secondselected = null;
        //        }
        //    }
        //}


        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            ClientTableViewCell cell = (ClientTableViewCell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetUpchargeList(ItemList[indexPath.Row]);
            return cell;
        }
    }
}
