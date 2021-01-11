using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class DB_PastHistory_Cell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DB_PastHistory_Cell");
        public static readonly UINib Nib;

        static DB_PastHistory_Cell()
        {
            Nib = UINib.FromName("DB_PastHistory_Cell", NSBundle.MainBundle);
        }

        protected DB_PastHistory_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(List<String> datalist, NSIndexPath indexPath)
        {
            PastHis_ShortView.Layer.CornerRadius = 5;
            PH_TicNo_Lbl.Text = "Ticket No:12345";
            PH_Date_Lbl.Text = "11/12/2020";
            PH_VehicleName_Lbl.Text = datalist[indexPath.Row];
            PH_Cost_Lbl.Text = "$250.00";
        }
    }
}
