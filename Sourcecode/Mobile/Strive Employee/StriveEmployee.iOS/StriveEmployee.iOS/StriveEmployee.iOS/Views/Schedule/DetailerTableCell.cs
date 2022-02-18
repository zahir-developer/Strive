using System;

using Foundation;
using Strive.Core.Models.Employee.Detailer;
using UIKit;

namespace StriveEmployee.iOS.Views.Schedule
{
    public partial class DetailerTableCell : UITableViewCell
    {
        public status data;
        public static readonly NSString Key = new NSString("DetailerTableCell");
        public static readonly UINib Nib;

        static DetailerTableCell()
        {
            Nib = UINib.FromName("DetailerTableCell", NSBundle.MainBundle);
           
        }

        protected DetailerTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public void updateData(status cellitem, NSIndexPath index)
        {
            data = cellitem;
            DetailCellView.Layer.CornerRadius = 5;
            lblTicketNumber.Text = data.Status[index.Row].TicketNumber;
            lblVehilce.Text = data.Status[index.Row].VehicleMake + "/" + data.Status[index.Row].VehicleModel + "/" + data.Status[index.Row].VehicleColor;
            lblDetailService.Text = data.Status[index.Row].DetailService;
            lblAdditionalService.Text = data.Status[index.Row].AdditionalService.Replace(" ", "");
            lblTimeIn.Text = DateTime.Parse(data.Status[index.Row].TimeIn).TimeOfDay.ToString();
            lblEstimateOut.Text = DateTime.Parse(data.Status[index.Row].EstimatedTimeOut).TimeOfDay.ToString(); 

        }
        


    }
}
