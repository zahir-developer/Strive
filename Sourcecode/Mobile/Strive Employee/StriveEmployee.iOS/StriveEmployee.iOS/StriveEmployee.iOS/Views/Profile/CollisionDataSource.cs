using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Employee.PersonalDetails;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public class CollisionDataSource : UITableViewSource
    {
        private List<EmployeeCollision> employeeCollisions = new List<EmployeeCollision>();
        public CollisionDataSource(List<EmployeeCollision> collisionList)
        {
            employeeCollisions = collisionList;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 100;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return employeeCollisions.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("CollisionCell", indexPath) as CollisionCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(indexPath, employeeCollisions);
            return cell;
        }
    }
}
