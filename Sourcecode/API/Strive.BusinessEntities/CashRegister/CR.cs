using System;

namespace Strive.BusinessEntities.CashRegister
{
    public class CR
    {
        public int CashRegisterId { get; set; }
        public int CashRegisterType { get; set; }
        public int LocationId { get; set; }
        public int DrawerId { get; set; }
        public int UserId { get; set; }
        public DateTime EnteredDateTime { get; set; }
        public int CashRegCoinId { get; set; }
        public int CashRegBillId { get; set; }
        public int CashRegRollId { get; set; }
        public int CashRegOtherId { get; set; }
    }
}
