using System;
using System.Collections.Generic;
using Foundation;
using Greeter.Cells;
using Greeter.DTOs;
using UIKit;

namespace Greeter.Sources
{
    public class IssuesSource : UITableViewSource
    {
        private VehicleIssueResponse issues;
        private List<VehicleIssue> VehicleIssue;
        public IssuesSource(VehicleIssueResponse Issues)
        {
            this.issues = Issues;
            this.VehicleIssue = Issues.VehicleIssueThumbnail.VehicleIssue;
            
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("IssueCell", indexPath) as IssueCell;
           
            cell.UpdateData(issues, indexPath);
            
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return VehicleIssue.Count;
        }
    }
}
