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
        public string TicketNumber { get; set; }
        public string CustomerName { get; set; }
        public string VehicleDescription { get; set; }
        public string ServiceName { get; set; }
        public decimal? Cost { get; set; }
        public string ServiceTypeName { get; set; }
        public string Checkin { get; set; }
        public string Checkout { get; set; }
        public string MembershipName { get; set; }
        public string PaymentStatus { get; set; }
        public string ColorCode { get; set; }
        public string MembershipNameOrPaymentStatus { get; set; }

    }
}
