using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.CashRegister
{
    public class CashRegisterCoin
    {
        public int CashRegCoinId { get; set; }
        public int? Pennies { get; set; }
        public int? Nickels { get; set; }
        public int? Dimes { get; set; }
        public int? Quarters { get; set; }
        public int? HalfDollars { get; set; }
        public DateTime? DateEntered { get; set; }

    }
}
