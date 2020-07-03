using Strive.BusinessEntities.CashRegister;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.CashRegister
{
    public interface ICashRegisterBpl
    {
        Result GetCashRegisterByDate(DateTime datetime);
        Result SaveTodayCashRegister(List<CashRegisterConsolidate> lstCashRegisterConsolidate);
    }
}
