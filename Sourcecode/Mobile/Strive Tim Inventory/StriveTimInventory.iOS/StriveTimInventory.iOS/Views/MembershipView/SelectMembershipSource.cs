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
            if (ClientTableViewCell.SelectedCell!=null)
            {
                ClientTableViewCell.SelectedCell.DeSelectMembershipcell();
            }
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
            //Adding 20$ if the selected membership price is less than previous membership
            
            if (MembershipData.SelectedMembership != null)
            {
                if (MembershipData.SelectedVehicle.MembershipName != null)
                {
                    var previousmembership = MembershipData.MembershipServiceList.Membership.FindAll(x => x.MembershipName == MembershipData.SelectedVehicle.MembershipName);
                    if (ViewModel.isDiscoutAvailable == true)
                    {
                        MembershipData.CalculatedPrice = MembershipData.SelectedMembershipPrice = (double)MembershipData.SelectedMembership.DiscountedPrice;
                        if (MembershipData.SelectedMembership.DiscountedPrice < previousmembership[0].DiscountedPrice)
                        {
                            MembershipData.CalculatedPrice += 20;
                            MembershipData.isMembershipSwitch = true;
                        }
                    }
                    else
                    {
                        MembershipData.CalculatedPrice = MembershipData.SelectedMembershipPrice = MembershipData.SelectedMembership.Price;
                        if (MembershipData.SelectedMembership.Price < previousmembership[0].Price)
                        {
                            MembershipData.CalculatedPrice += 20;
                            MembershipData.isMembershipSwitch = false;
                        }
                    }
                    
                }
                else
                {
                    if (ViewModel.isDiscoutAvailable == true)
                    {
                        MembershipData.CalculatedPrice = MembershipData.SelectedMembershipPrice = (double)MembershipData.SelectedMembership.DiscountedPrice;
                    }
                    else
                    {
                        MembershipData.CalculatedPrice = MembershipData.SelectedMembershipPrice = MembershipData.SelectedMembership.Price;
                    }
                }

            }
            //

        }
        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            ClientTableViewCell cell = (ClientTableViewCell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetMembershipList(ItemList[indexPath.Row],indexPath.Row,Selectedindex,cell);
            return cell;
        }
    }
}
