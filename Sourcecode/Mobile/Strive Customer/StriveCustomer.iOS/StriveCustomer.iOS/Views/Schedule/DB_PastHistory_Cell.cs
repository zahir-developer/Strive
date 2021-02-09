﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Foundation;
using Strive.Core.Models.Customer;
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

        public void SetData(List<BayJobDetailViewModel> datalist, NSIndexPath indexPath)
        {            
            PastHis_FullView.Layer.CornerRadius = 5;
            PH_VehicleName_Lbl.Text = datalist[indexPath.Row].VehicleColor +
                                        datalist[indexPath.Row].VehicleColor +
                                        datalist[indexPath.Row].VehicleModel;
            
            var date = datalist[indexPath.Row].JobDate;            
            var FullSplitDates = date.Split("-");
            var fullDateInfo = FullSplitDates[2].Substring(0,2);

            PH_Date_Lbl.Text = FullSplitDates[1].ToString() +"/"+ fullDateInfo.ToString() +"/"+ FullSplitDates[0].ToString();
            PH_DetService_Lbl.Text = "";
            PH_Barcode_Lbl.Text = "";
            PH_Price_Lbl.Text = datalist[indexPath.Row].Cost.ToString();
        }      
    }
}
