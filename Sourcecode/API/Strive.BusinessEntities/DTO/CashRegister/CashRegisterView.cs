using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strive.BusinessEntities.CashRegister
{
    public class CashRegisterView : CashRegister
    {
        public CashRegisterCoin CashRegisterCoin { get; set; }
        public CashRegisterRoll CashRegisterRoll { get; set; }
        public CashRegisterBill CashRegisterBill { get; set; }
        public CashRegisterOther CashRegisterOther { get; set; }
    }
}

