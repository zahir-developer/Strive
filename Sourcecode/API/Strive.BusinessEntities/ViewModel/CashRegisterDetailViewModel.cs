using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
   public class CashRegisterDetailViewModel
    {

        public CashRegisterViewModel CashRegister { get; set; }
        public CashRegisterCoinsViewModel CashRegisterCoins { get; set; }
        public CashRegisterRollsViewModel CashRegisterRolls { get; set; }
        public CashRegisterBillsViewModel CashRegisterBills { get; set; }
        public CashRegisterOthersViewModel CashRegisterOthers { get; set; }

        

    }
   
}
