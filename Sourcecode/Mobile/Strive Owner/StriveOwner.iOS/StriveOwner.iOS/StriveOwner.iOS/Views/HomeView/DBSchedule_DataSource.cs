using System;
using Foundation;
using Strive.Core.Models.Customer;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.Base;
using UIKit;

namespace StriveOwner.iOS.Views.HomeView
{
    public class DBSchedule_DataSource : MvxTableViewSource
    {
        private static string CellId = "DBSchedule_Cell";
        ScheduleModel list;
        public static bool isexpanded = false;
        public DBSchedule_DataSource(UITableView tableView, ScheduleModel scheduleList) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(DBSchedule_Cell.Nib, CellId);
            list = scheduleList;
        }        

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return list.DetailsGrid.BayJobDetailViewModel.Count;
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            var itm = list.DetailsGrid.BayJobDetailViewModel[indexPath.Row];
            return itm;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return isexpanded ? DBSchedule_Cell.ExpandedHeight:DBSchedule_Cell.NormalHeight; 
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var item = list.DetailsGrid.BayJobDetailViewModel[indexPath.Row];
            var cell = GetOrCreateCellFor(tableView, indexPath, item);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            //var cell = tableView.DequeueReusableCell("DBSchedule_Cell", indexPath) as DBSchedule_Cell;
            //cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            //cell.SetupView(list.DetailsGrid.BayJobDetailViewModel[indexPath.Row], isexpanded);
            //cell.SetupCell(() =>
               //tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.None));
            return cell;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            DBSchedule_Cell cell = (DBSchedule_Cell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetupView(list.DetailsGrid.BayJobDetailViewModel[indexPath.Row], isexpanded);
            cell.SetupCell(isexpanded, () =>
               tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.None));
            return cell;
        }        
    }
}
