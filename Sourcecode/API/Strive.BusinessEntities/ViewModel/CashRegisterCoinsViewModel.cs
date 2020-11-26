using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class CashRegisterCoinsViewModel
    {

        public int CashRegCoinId { get; set; }
        public int CashRegisterId { get; set; }
        public decimal Pennies { get; set; }
        public decimal Nickels { get; set; }
        public decimal Dimes { get; set; }
        public decimal Quarters { get; set; }
        public decimal HalfDollars { get; set; }

    }
}

