using System;

using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.Customer;
using UIKit;

namespace StriveOwner.iOS.Views.HomeView
{
    public partial class DBSchedule_Cell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("DBSchedule_Cell");
        public static readonly UINib Nib;
        DBSchedule_Cell cell;

        public static readonly int ExpandedHeight = 260;
        public static readonly int NormalHeight = 60;
        private Action reloadParentRow { get; set; }
        public bool isexpanded;

        static DBSchedule_Cell()
        {
            Nib = UINib.FromName("DBSchedule_Cell", NSBundle.MainBundle);
        }

        protected DBSchedule_Cell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
            
        }

        public void SetupView(BayJobDetailViewModel item, Boolean expand)
        {
            isexpanded = expand;
            BayNameLbl.Text = item.BayName;
            TicketNo_Lbl.Text = "Ticket#: " + item.TicketNumber;
            TimeInValue.Text = item.TimeIn;
            ClientValue.Text = item.ClientName;
            PhoneValue.Text = item.PhoneNumber;
            TimeOutValue.Text = item.EstimatedTimeOut;
            MakeValue.Text= item.VehicleMake+"/"+item.VehicleModel+"/"+item.VehicleColor;
            ServiceValue.Text = item.ServiceTypeName;
            UpchargeValue.Text= item.Upcharge.ToString();
            TicketView.Hidden = false;
        }

        partial void BayBtn_Touch(UIButton sender)
        {
            //if(ticketView_HeightConst.Constant == 0)
            //{
            //    showTicketView();
            //    isexpanded = true;
            //}
            //else
            //{
            //    hideTicketView();
            //    isexpanded = false;
            //}
            //LayoutSubviews();
            //reloadParentRow();
        }

        //public void SetupCell(bool expand, Action reloadParentRow)
        //{
        //    this.isexpanded = expand;
        //    this.reloadParentRow = reloadParentRow;

        //    if (isexpanded)
        //    {
        //        showTicketView();
        //    }
        //    else
        //    {
        //        hideTicketView();
        //    }
        //}


        //public void showTicketView()
        //{
        //    ticketView_HeightConst.Constant = 200;
        //    TicketView.Hidden = false;
        //}

        //public void hideTicketView()
        //{
        //    ticketView_HeightConst.Constant = 0;
        //    TicketView.Hidden = false;
        //}

        //public void ExpandView(bool isExpand)
        //{
        //    if (isExpand)
        //    {
        //        ticketView_HeightConst.Constant = 200;
        //        TicketView.Hidden = false;
        //    }
        //    else
        //    {
        //        ticketView_HeightConst.Constant = 0;
        //        TicketView.Hidden = false;
        //    }
        //}
    }
}
