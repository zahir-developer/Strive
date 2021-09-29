using System;
using CoreFoundation;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class VehicleListTableSource : UITableViewSource
    {
        public VehicleList vehicleLists;
        public UITableView vehicleTable = new UITableView();
        private VehicleInfoViewModel vehicleViewModel;
        public VehicleListTableSource(VehicleInfoViewModel viewModel)
        {
            this.vehicleViewModel = viewModel;
            this.vehicleLists = viewModel.vehicleLists;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 80;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return vehicleLists.Status.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("VehicleListViewCell", indexPath) as VehicleListViewCell;
            vehicleTable = tableView;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(vehicleLists, indexPath);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            VehicleListViewCell cell = (VehicleListViewCell)tableView.CellAt(indexPath);
            //CustomerInfo.SelectedVehiclePastDetails = services.PastClientDetails[indexPath.Row].VehicleId;            
        }

        public async void deleteRow(NSIndexPath selectedRow)
        {
            if(CustomerInfo.actionType == 1)
            {
                var vehicleViewModel = new VehicleInfoViewModel();
                var data = CustomerVehiclesInformation.vehiclesList.Status[selectedRow.Row];
                var deleted = await vehicleViewModel.DeleteCustomerVehicle(data.VehicleId);
                if (deleted)
                {                    
                    vehicleTable.DeleteRows(new NSIndexPath[] { selectedRow}, UITableViewRowAnimation.Fade);              
                    vehicleViewModel.NavToProfile();                    
                }
            }            
        }

        public void editVehicleList(NSIndexPath indexPath)
        {
            var data = CustomerVehiclesInformation.vehiclesList.Status[indexPath.Row];
            CustomerVehiclesInformation.selectedVehicleInfo = data.VehicleId;
            MembershipDetails.clientVehicleID = data.VehicleId;
            MembershipDetails.modelNumber = data.VehicleModelId;

            vehicleViewModel.NavToEditVehicle();
        }
    }
}