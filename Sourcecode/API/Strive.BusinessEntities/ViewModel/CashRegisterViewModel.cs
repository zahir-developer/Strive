using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
   public class CashRegisterViewModel
    {
        public int CashRegisterId { get; set; }

        public int? CashRegisterType { get; set; }

        public int? LocationId { get; set; }

        public int? DrawerId { get; set; }

        public DateTime? StoreTimeIn { get; set; }
        public DateTime? StoreTimeOut { get; set; }
        public int? StoreOpenCloseStatus { get; set; }
        public decimal? Tips { get; set; }
        public decimal? TotalAmount { get; set; }



    }
}
