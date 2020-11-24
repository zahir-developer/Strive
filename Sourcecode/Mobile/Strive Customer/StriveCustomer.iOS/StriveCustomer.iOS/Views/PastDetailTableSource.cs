using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Foundation;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class PastDetailTableSource : UITableViewSource
    {
        private static string CellId = "PastDetailViewCell";

        private PastDetailViewModel ViewModel;
        private PastClientServices services = new PastClientServices();
        private MvxViewController view;

        
        public PastDetailTableSource(MvxViewController profileView, PastClientServices clientServices) 
        {
            this.view = profileView;
            this.services = clientServices;                                    
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
            return services.PastClientDetails.Count();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("PastDetailViewCell", indexPath) as PastDetailViewCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(services, indexPath);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            PastDetailViewCell cell = (PastDetailViewCell)tableView.CellAt(indexPath);
            CustomerInfo.SelectedVehiclePastDetails = services.PastClientDetails[indexPath.Row].VehicleId;
            var pastTabView = new PastDetailTabView();
            view.NavigationController.PushViewController(pastTabView, true);
        }

    }
}
