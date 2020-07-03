using System;

namespace Strive.BusinessEntities.CashRegister
{
    public class CashRegisterBill
    {
        public int CashRegBillId { get; set; }
        public int? Ones { get; set; }
        public int? Fives { get; set; }
        public int? Tens { get; set; }
        public int? Twenties { get; set; }
        public int? Fifties { get; set; }
        public int? Hundreds { get; set; }
        public DateTime? DateEntered { get; set; }

    }
}
