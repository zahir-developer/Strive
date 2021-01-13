using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public class Schedule_PastHis_Source : UITableViewSource
    {
        public List<String> PastHis_List = new List<string>();
        public Schedule_PastHis_Source()
        {
            PastHis_List.Add("New York");
            PastHis_List.Add("Japan");
            PastHis_List.Add("Chicago");
            PastHis_List.Add("Washington");
            PastHis_List.Add("Mexico");
            PastHis_List.Add("Toronto");
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 90;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return PastHis_List.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DB_PastHistory_Cell", indexPath) as DB_PastHistory_Cell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(PastHis_List, indexPath);
            return cell;
        }
    }
}
