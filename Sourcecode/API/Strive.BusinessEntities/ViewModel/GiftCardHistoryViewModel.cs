using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class GiftCardHistoryViewModel
    {
        public int? GiftCardHistoryId { get; set; }
        public int? GiftCardId { get; set; }
        public int? TransactionType { get; set; }
        public decimal? TransactionAmount { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool Status { get; set; }

        public string TicketNumber { get; set; }
    }
}
