using Strive.BusinessEntities.Model;

namespace Strive.BusinessEntities.CashRegister.DTO
{
    public class CashRegisterDto
    {
        public Model.CashRegister CashRegister { get; set; }
        public CashRegisterCoins CashRegisterCoin { get; set; }
        public CashRegisterRolls CashRegisterRoll { get; set; }
        public CashRegisterBills CashRegisterBill { get; set; }
        public CashRegisterOthers CashRegisterOther { get; set; }
    }
}

