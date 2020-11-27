using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CashRegisterOthersViewModel
    {
        public int CashRegOtherId { get; set; }
        public int CashRegisterId { get; set; }
        
        public decimal? CreditCard1 { get; set; }        
        public decimal? CreditCard2 { get; set; }
               
        public decimal? CreditCard3 { get; set; }
               
        public decimal? Checks { get; set; }
    }
}
