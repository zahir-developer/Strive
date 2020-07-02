namespace Strive.BusinessEntities.CashRegister
{
    public class CashInRegister
    {
        public int Ones { get; set; }
        public int Fives { get; set; }
        public int Tens { get; set; }
        public int Twenties { get; set; }
        public int Fifties { get; set; }
        public int Hundreds { get; set; }
        public int Pennies { get; set; }
        public int Nickels { get; set; }
        public int Dimes { get; set; }
        public int Quarters { get; set; }
        public int HalfDollars { get; set; }
        public decimal CreditCard1 { get; set; }
        public decimal CreditCard2 { get; set; }
        public decimal CreditCard3 { get; set; }
        public decimal Checks { get; set; }
        public decimal Payouts { get; set; }
        public int PenniesCount { get; set; }
        public int NickelsCount { get; set; }
        public int DimesCount { get; set; }
        public int QuartersCount { get; set; }
        public int HalfDollarsCount { get; set; }
    }
}
