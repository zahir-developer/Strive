using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Employee.PersonalDetails;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public class DocumentDataSource : UITableViewSource
    {
        List<EmployeeDocument> employeeDocuments; 
        public DocumentDataSource(List<EmployeeDocument> documentsList)
        {
            employeeDocuments = documentsList;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 120;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DocumentsCell", indexPath) as DocumentsCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(indexPath, employeeDocuments);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return employeeDocuments.Count;
        }
    }
}
