using System;
using System.Collections.Generic;
using System.Globalization;
using Foundation;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views.Schedule
{
    public partial class DB_PastHistory_Cell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DB_PastHistory_Cell");
        public static readonly UINib Nib;
        Schedule_PastHis_Source source;
        NSIndexPath SelectedIndex;
        private ScheduleViewModel viewModel = new ScheduleViewModel();
        float Price;
        static DB_PastHistory_Cell()
        {
            Nib = UINib.FromName("DB_PastHistory_Cell", NSBundle.MainBundle);
        }

        protected DB_PastHistory_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(List<jobViewModel> datalist, NSIndexPath indexPath)
        {
            TipAmount.Hidden = true;
            TipAmount.Layer.CornerRadius = 4;
            SelectedIndex = indexPath;
            PayTip.Layer.CornerRadius = 10;
            PastHis_FullView.Layer.CornerRadius = 5;
            PH_TicNo_Lbl.Text = datalist[indexPath.Row].TicketNumber;
            PH_VehicleName_Lbl.Text = datalist[indexPath.Row].VehicleMake + "/" +
                                        datalist[indexPath.Row].VehicleModel + "/" +
                                        datalist[indexPath.Row].VehicleColor;
            
            var date = datalist[indexPath.Row].JobDate;            
            var FullSplitDates = date.Split("-");
            var fullDateInfo = FullSplitDates[2].Substring(0,2);

            PH_Date_Lbl.Text = FullSplitDates[1].ToString() +"/"+ fullDateInfo.ToString() +"/"+ FullSplitDates[0].ToString();
            PH_DetService_Lbl.Text = datalist[indexPath.Row].ServiceTypeName;
            PH_Barcode_Lbl.Text = datalist[indexPath.Row].Barcode;
            PH_Price_Lbl.Text = datalist[indexPath.Row].Cost.ToString();
            if (datalist[indexPath.Row].AdditionalServices != null)
            {
                Additional_Services.Text = datalist[indexPath.Row].AdditionalServices.Replace(" ", "");
            }
            else
            {
                Additional_Services.Text = "No Added Service";
            }
            
            // Price = datalist[indexPath.Row].Cost;
            //ScheduleViewModel.VehicleId = 214796;
            //ScheduleViewModel.JobID = datalist[indexPath.Row].JobId;
            //ScheduleViewModel.TicketNumber = datalist[indexPath.Row].TicketNumber;
            //PH_Cost_Lbl.Text = datalist[indexPath.Row].Cost.ToString();

            if (datalist[indexPath.Row].PaymentDate != null)
            {
                if (datalist[indexPath.Row].PaymentDate.Substring(0, 10) == DateTime.Now.Date.ToString("yyyy-MM-dd"))
                {
                    
                    if (DateTime.Now.TimeOfDay.Hours >= 20)
                    {
                        PayTip.Hidden = true;
                    }
                    else
                    {
                        if (datalist[indexPath.Row].TipAmount != "0.00")
                        {
                            PayTip.Hidden = true;
                            if (ScheduleViewModel.IsDetail != true)
                            {
                                TipAmount.Hidden = false;
                                TipAmount.Text = "Tip Added:$" + datalist[indexPath.Row].TipAmount;
                            }

                        }
                        else
                        {
                            if (ScheduleViewModel.IsDetail == true)
                            {
                                PayTip.Hidden = true;
                            }
                            else
                            {
                                PayTip.Hidden = false;
                            }

                        }

                    }

                }
                else
                {
                    PayTip.Hidden = true;
                }
            }
            else
            {
                PayTip.Hidden = true;
            }
        }
        partial void Pay_BtnTouch(UIButton sender)
        {
            Schedule_PastHis_Source source = new Schedule_PastHis_Source(viewModel);
            source.AddWashTip(SelectedIndex);
            //var dict = new NSDictionary(new NSString("Price"), new NSString(Price.ToString()));
            //NSNotificationCenter.DefaultCenter.PostNotificationName(new NSString("com.strive.employee.Pay"), null,dict);
        }

    }
}
