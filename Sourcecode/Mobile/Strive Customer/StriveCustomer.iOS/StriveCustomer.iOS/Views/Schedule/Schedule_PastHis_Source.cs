using System;
using System.Collections.Generic;
using System.Linq;
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
        public NSIndexPath selectedCell;
        public List<BayJobDetailViewModel> PastHis_List = new List<BayJobDetailViewModel>();
        public Schedule_PastHis_Source(ScheduleViewModel viewModel)
        {
            this.ViewModel = viewModel;
            var JobDetailsViewModel = ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel;
            this.PastHis_List = JobDetailsViewModel.OrderByDescending(x => DateTime.Parse(x.JobDate)).ToList();

            //this.PastHis_List = (List<BayJobDetailViewModel>)ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel.OrderByDescending(x => DateTime.Parse(x.JobDate)); ;


        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            if ((bool)PastHis_List[indexPath.Row].IsOpened)
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
            tableView.RowHeight = 200;

            return cell;
        }
        
        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            
            if (!(bool)PastHis_List[indexPath.Row].IsOpened)
            {
                PastHis_List[indexPath.Row].IsOpened = true;
                var cell = tableView.DequeueReusableCell("DB_PastHistory_Cell", indexPath) as DB_PastHistory_Cell;
                cell.SetData(PastHis_List, indexPath);
                GetHeightForRow(tableView, indexPath);
                selectedCell = indexPath;
            }
            else
            {
                PastHis_List[indexPath.Row].IsOpened = false;
                var cell = tableView.DequeueReusableCell("DB_PastHistory_Cell", indexPath) as DB_PastHistory_Cell;
                cell.SetData(PastHis_List, indexPath);
                GetHeightForRow(tableView, indexPath);
                selectedCell = indexPath;
                cell.Selected = false;
            }
            
        }

        //public override void RowDeselected(UITableView tableView, NSIndexPath indexPath) commented for now becasue of crash
        //{
        //    isClicked = false;
        //    var cell = tableView.DequeueReusableCell("DB_PastHistory_Cell", indexPath) as DB_PastHistory_Cell;
        //    GetHeightForRow(tableView, indexPath);
        //}
    }
}
