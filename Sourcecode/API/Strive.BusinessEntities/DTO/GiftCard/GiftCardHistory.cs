using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.GiftCard
{
    public class GiftCardHistory
    {
        public int GiftCardHistoryId { get; set; }
        public int GiftCardId { get; set; }
        public int LocationId { get; set; }
        public int TransactionType { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Comments { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
