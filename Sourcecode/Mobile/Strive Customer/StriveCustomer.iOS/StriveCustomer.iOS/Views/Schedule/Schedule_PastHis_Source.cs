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
        
        public List<jobViewModel> PastHis_List = new List<jobViewModel>();
        public static List<jobViewModel> SavedList = new List<jobViewModel>();
        public Schedule_PastHis_Source(ScheduleViewModel viewModel)
        {
            this.ViewModel = viewModel;
            if (viewModel.pastServiceHistory != null)
            {
                var JobDetailsViewModel = ViewModel.pastServiceHistory.DetailsGrid.JobViewModel;
                this.PastHis_List = JobDetailsViewModel.OrderByDescending(x => DateTime.Parse(x.JobDate)).ToList();
                SavedList = PastHis_List;
            }
            //var JobDetailsViewModel = ViewModel.pastServiceHistory.DetailsGrid.BayJobDetailViewModel;
            //this.PastHis_List = JobDetailsViewModel.OrderByDescending(x => DateTime.Parse(x.JobDate)).ToList();
            //test = PastHis_List;
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
            return ViewModel.pastServiceHistory.DetailsGrid.JobViewModel.Count;
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

       public void AddWashTip( NSIndexPath indexPath)
        {
            float Price = SavedList[indexPath.Row].Cost;
            ScheduleViewModel.VehicleId = int.Parse(SavedList[indexPath.Row].VehicleId);
            ScheduleViewModel.Jobid = SavedList[indexPath.Row].JobId;
            ScheduleViewModel.TicketNumber = SavedList[indexPath.Row].TicketNumber;
            ScheduleViewModel.JobPaymentId = int.Parse(SavedList[indexPath.Row].JobPaymentId);
            var dict = new NSDictionary(new NSString("Price"), new NSString(Price.ToString()));
            NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.employee.Pay"), null, dict);
        }

        //public override void RowDeselected(UITableView tableView, NSIndexPath indexPath) commented for now becasue of crash
        //{
        //    isClicked = false;
        //    var cell = tableView.DequeueReusableCell("DB_PastHistory_Cell", indexPath) as DB_PastHistory_Cell;
        //    GetHeightForRow(tableView, indexPath);
        //}
    }
}
