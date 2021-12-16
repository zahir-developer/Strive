using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientActivityPaymentHistoryViewModel
    {
        public int? TicketNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ServiceCompleted { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public string Detailer { get; set; }
        public decimal CommissionAmount { get; set; }
    }
}
