using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public class Schedule_PastHis_Source : UITableViewSource
    {
        ScheduleViewModel ViewModel;
        public bool isClicked = false;
        public List<BayJobDetailViewModel> PastHis_List = new List<BayJobDetailViewModel>();
        public Schedule_PastHis_Source(ScheduleViewModel viewModel)
        {
            this.ViewModel = viewModel;
            this.PastHis_List = ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel;

        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if (isClicked)
            {
                return 200;
            }
            else
            {
                return 90;
            }            
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DB_PastHistory_Cell", indexPath) as DB_PastHistory_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(PastHis_List, indexPath);
            return cell;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            isClicked = true;
            var cell = tableView.DequeueReusableCell("DB_PastHistory_Cell", indexPath) as DB_PastHistory_Cell;
           
            GetHeightForRow(tableView, indexPath);                       
        }        
    }
}
