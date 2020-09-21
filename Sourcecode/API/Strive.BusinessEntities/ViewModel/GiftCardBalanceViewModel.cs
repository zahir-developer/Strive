using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class GiftCardBalanceViewModel
    {
        public int GiftCardId { get; set; }
        public decimal BalaceAmount { get; set; }
        public DateTimeOffset ActiveDate { get; set; }
    }
}
