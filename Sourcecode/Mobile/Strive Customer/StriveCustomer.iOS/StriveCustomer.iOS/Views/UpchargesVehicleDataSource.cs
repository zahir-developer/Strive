using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Binding.Extensions;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class UpchargesVehicleDataSource : UITableViewSource
    {
        private ServiceList upchargeList;
        private List<string> recentUpcharge = new List<string>();
        public UpchargesVehicleDataSource(ServiceList list)
        {
            this.upchargeList = list;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 50;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            var index = 0;
            foreach(var item in upchargeList.ServicesWithPrice)
            {
                if ((upchargeList.ServicesWithPrice[index].Upcharges != null) && (!recentUpcharge.Contains(upchargeList.ServicesWithPrice[index].Upcharges)))
                {
                    recentUpcharge.Add(upchargeList.ServicesWithPrice[index].Upcharges);
                }
                index++;
            }           
            return recentUpcharge.Count;
        }   

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MembershipVehicle_ViewCell", indexPath) as MembershipVehicle_ViewCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
                        
            //if ((upchargeList.ServicesWithPrice[indexPath.Row].Upcharges != null) || (!recentUpcharge.Contains(upchargeList.ServicesWithPrice[indexPath.Row].Upcharges)))
            //{
                cell.setUpchargeData(recentUpcharge, indexPath, cell);
            //    recentUpcharge.Add(upchargeList.ServicesWithPrice[indexPath.Row].Upcharges);
            //}            
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            MembershipVehicle_ViewCell cell = (MembershipVehicle_ViewCell)tableView.CellAt(indexPath);
            cell.updateCell(indexPath);
            foreach(var item in upchargeList.ServicesWithPrice)
            {
                if (recentUpcharge[indexPath.Row] == item.Upcharges)
                {
                    MembershipDetails.selectedUpCharge = item.ServiceId;
                }
            }
        }

        public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        {
            MembershipVehicle_ViewCell cell = (MembershipVehicle_ViewCell)tableView.CellAt(indexPath);
            cell.deselectRow(indexPath);
        }
    }
}
