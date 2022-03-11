using System;
using System.Collections.Generic;
using CoreText;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class MembershipVehicleDataSource : UITableViewSource
    {
        public MembershipServiceList data;              

        public MembershipVehicleDataSource(MembershipServiceList membershipList)
        {
            data = membershipList;            
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
            return data.Membership.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MembershipVehicle_ViewCell", indexPath) as MembershipVehicle_ViewCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(data, indexPath);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {                       
            MembershipVehicle_ViewCell cell = (MembershipVehicle_ViewCell)tableView.CellAt(indexPath);
            cell.updateServices(indexPath);            
            MembershipDetails.selectedMembership = data.Membership[indexPath.Row].MembershipId;
            UpdatePrice(indexPath);
        }

        //public override void RowDeselected(UITableView tableView, NSIndexPath indexPath)
        //{
        //    MembershipVehicle_ViewCell cell = (MembershipVehicle_ViewCell)tableView.CellAt(indexPath);
        //    cell.deselectRow(indexPath);
        //}

        public void UpdatePrice(NSIndexPath indexPath)
        {
            if (MembershipDetails.selectedMembershipDetail!=null)
            {
                if (MembershipDetails.selectedMembershipDetail.Price > data.Membership[indexPath.Row].Price)
                {
                    CustomerInfo.MembershipFee = 20;
                    MembershipDetails.selectedMembershipDetail = data.Membership[indexPath.Row];

                }
                else
                {
                    CustomerInfo.MembershipFee = 0;
                    MembershipDetails.selectedMembershipDetail = data.Membership[indexPath.Row];
                }
            }
            else
            {
                CustomerInfo.MembershipFee = 0;
                MembershipDetails.selectedMembershipDetail = data.Membership[indexPath.Row];
            }

            
        }


    }
}
