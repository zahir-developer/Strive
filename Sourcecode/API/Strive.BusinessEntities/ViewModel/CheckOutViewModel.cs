using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CheckOutViewModel
    {
        public int JobId { get; set; }
        public string valuedesc { get; set; }
        public int? JobPaymentId { get; set; }
        public string OutsideService { get; set; }
        public string TicketNumber { get; set; }
        public string CustomerFirstName { get; set; }

        public string CustomerLastName { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }

        public string VehicleDescription { get; set; }
        public string AdditionalServices { get; set; }
        public string Services { get; set; }
        public decimal? Cost { get; set; }
        public string Checkin { get; set; }
        public string Checkout { get; set; }
        public string MembershipName { get; set; }
        public string PaymentStatus { get; set; }
        public string JobStatus { get; set; }
        public string JobType { get; set; }
        public string ColorCode { get; set; }
        public string MembershipNameOrPaymentStatus { get; set; }
        public bool? IsHold { get; set; }

    }
}
