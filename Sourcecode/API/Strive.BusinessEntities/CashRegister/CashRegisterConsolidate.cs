using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.CashRegister
{
    public class CashRegisterConsolidate
    {
        public long CashRegBillId { get; set; }
        public int Ones { get; set;}
        public int Fives { get; set; }
        public int Tens { get; set; }
        public int Twenties { get; set; }
        public int Fifties { get; set; }
        public int Hundreds { get; set; }
        public DateTime DateEnteredCRB { get; set; }
        public int CashRegCoinId { get; set; }
        public int Pennies { get; set; }
        public int Nickels { get; set; }
        public int Dimes { get; set; }
        public int Quarters { get; set; }
        public int HalfDollars { get; set; }
        public DateTime DateEnteredCRC { get; set; }
        public int CashRegOtherId { get; set; }
        public decimal CreditCard1 { get; set; }
        public decimal CreditCard2 { get; set; }
        public decimal CreditCard3 { get; set; }
        public decimal Checks { get; set; }
        public decimal Payouts { get; set; }
        public DateTime DateEnteredCRO { get; set; }
        public int CashRegRollId { get; set; }
        public int PenniesCount { get; set; }
        public int NickelsCount { get; set; }
        public int DimesCount { get; set; }
        public int QuartersCount { get; set; }
        public int HalfDollarsCount { get; set; }
        public DateTime DateEnteredCRR { get; set; }

    }
}
