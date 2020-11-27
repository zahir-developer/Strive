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
    }
}
