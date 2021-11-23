using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class AdditionalServicesDataSource : UITableViewSource
    {
        private ObservableCollection<AllServiceDetail> services = new ObservableCollection<AllServiceDetail>();
        public AdditionalServicesDataSource(ObservableCollection<AllServiceDetail> services)
        {
            if (MembershipDetails.selectedAdditionalServices == null || MembershipDetails.selectedAdditionalServices.Count == 0)
            {
                MembershipDetails.selectedAdditionalServices = new List<int>();
            }

            foreach (var data in services)
            {
                if (string.Equals(data.ServiceTypeName, "Additional Services"))
                {
                    this.services.Add(data);
                }
            }
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
            return services.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("MembershipVehicle_ViewCell", indexPath) as MembershipVehicle_ViewCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.setServicesData(services, indexPath, cell);                  
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
             MembershipVehicle_ViewCell cell = (MembershipVehicle_ViewCell)tableView.CellAt(indexPath);
             cell.updateServices(indexPath);
            if (MembershipDetails.selectedAdditionalServices.Count!=0)
            {
                if (MembershipDetails.selectedAdditionalServices.Contains(services[indexPath.Row].ServiceId))
                {
                    MembershipDetails.selectedAdditionalServices.Remove(services[indexPath.Row].ServiceId);
                }
                else
                {
                    MembershipDetails.selectedAdditionalServices.Add(services[indexPath.Row].ServiceId);
                }

            }
            else
            {
                MembershipDetails.selectedAdditionalServices.Add(services[indexPath.Row].ServiceId);
            }
            
             
        }
    }
}
