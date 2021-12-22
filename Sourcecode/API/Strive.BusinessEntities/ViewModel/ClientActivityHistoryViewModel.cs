using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
   public class ClientActivityHistoryViewModel
    {
        public int? CreditAccountHistoryId { get; set; }
        public int? CreditAccountId { get; set; }
        public int? TransactionType { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool Status { get; set; }
        public string Comments { get; set; }
        public string TicketNumber { get; set; }
        public int? ClientId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
