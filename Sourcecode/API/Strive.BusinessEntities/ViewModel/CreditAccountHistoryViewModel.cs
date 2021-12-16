using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CreditAccountHistoryViewModel
    {
        public int? CreditAccountHistoryId { get; set; }
        public int? CreditAccountId { get; set; }
        public int? TransactionType { get; set; }
        public decimal? Amount { get; set; }        
        public string TicketNumber { get; set; }
        public bool Status { get; set; }
    }
}
