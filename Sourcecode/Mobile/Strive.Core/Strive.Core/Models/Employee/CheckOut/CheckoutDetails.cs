using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.CheckOut
{
    public class CheckoutDetails
    {
        public GetCheckedInVehicleDetails GetCheckedInVehicleDetails { get; set; }
    }

    public class GetCheckedInVehicleDetails
    {
        public List<checkOutViewModel> checkOutViewModel { get; set; }
    }

    public class checkOutViewModel
    {
        public int? JobId { get; set; }
        public string valuedesc { get; set; }
        public int? JobPaymentId { get; set; }
        public string OutsideService { get; set; }
        public string TicketNumber { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
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

    public class holdCheckoutReq
    {
        public int id { get; set; }
        public bool isHold { get; set; }
    }

    public class holdCheckoutResponse
    {
        public bool UpdateJobStatus { get; set; }
    }

    public class completeCheckoutReq
    {
        public int jobId { get; set; }
        public DateTime actualTimeOut { get; set; }
    }

    public class doCheckoutReq
    {
        public int jobId { get; set; }
        public bool checkOut { get; set; }
        public DateTime checkOutTime { get; set; }
    }

    public class CheckoutResponse
    {
        public bool SaveCheckoutTime { get; set; }
    }
}
