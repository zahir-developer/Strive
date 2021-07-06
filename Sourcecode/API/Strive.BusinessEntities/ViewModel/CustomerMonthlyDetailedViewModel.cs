using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CustomerMonthlyDetailedViewModel
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int JobId { get; set; }
        public string TicketNumber { get; set; }
        public string Model { get; set; }
        public string ModelDescription { get; set; }
        public string Color { get; set; }
        public string ModelColor { get; set; }
        public DateTime JobDate { get; set; }
        public int MemberShipId { get; set; }
        public decimal MembershipPrice { get; set; }
        public int PaymentType { get; set; }
        public string PaymentDescription { get; set; }
        public string MemberShipName { get; set; }
        public decimal TicketAmount { get; set; }
        public decimal DifferenceAmount { get; set; }

    }
}
