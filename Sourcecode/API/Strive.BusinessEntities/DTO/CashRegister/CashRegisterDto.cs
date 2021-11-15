using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;

namespace Strive.BusinessEntities.CashRegister.DTO
{
    public class CashRegisterDto
    {
        public Model.CashRegister CashRegister { get; set; }
        public CashRegisterCoins CashRegisterCoins { get; set; }
        public CashRegisterRolls CashRegisterRolls { get; set; }
        public CashRegisterBills CashRegisterBills { get; set; }
        public CashRegisterOthers CashRegisterOthers { get; set; }
        public CardAmountViewModel CardAmount { get; set; }

    }



}

