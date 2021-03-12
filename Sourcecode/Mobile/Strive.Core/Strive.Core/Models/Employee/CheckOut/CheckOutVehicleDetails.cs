using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.CheckOut
{
    public class CheckOutVehicleDetails
    {
        public List<GetCheckedInVehicleDetails> GetCheckedInVehicleDetails { get; set; }
    }
    public class GetCheckedInVehicleDetails
    {
        public int JobId { get; set; }
        public int JobPaymentId { get; set; }
        public string TicketNumber { get; set; }
        public string CustomerName { get; set; }
        public string VehicleDescription { get; set; }
        public string AdditionalServices { get; set; }
        public string Services { get; set; }
        public float? Cost { get; set; }
        public string Checkin { get; set; }
        public string Checkout { get; set; }
        public string MembershipName { get; set; }
        public string PaymentStatus { get; set; }
        public string ColorCode { get; set; }
        public string MembershipNameOrPaymentStatus { get; set; }

    }
}
