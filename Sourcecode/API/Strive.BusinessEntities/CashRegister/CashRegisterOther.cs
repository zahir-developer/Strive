using System;

namespace Strive.BusinessEntities.CashRegister
{
    public class CashRegisterOther
    {
        public int CashRegOtherId { get; set; }
        public Decimal? CreditCard1 { get; set; }
        public Decimal? CreditCard2 { get; set; }
        public Decimal? CreditCard3 { get; set; }
        public Decimal? Checks { get; set; }
        public Decimal? Payouts { get; set; }
        public DateTime? DateEntered { get; set; }
    }
}
