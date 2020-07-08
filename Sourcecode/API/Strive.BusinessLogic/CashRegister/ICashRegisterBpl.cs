using Strive.BusinessEntities.CashRegister;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic.CashRegister
{
    public interface ICashRegisterBpl
    {
        Result GetCashRegisterDetails(CashRegisterType cashRegisterType, int locationId, DateTime dateTime);
        Result SaveTodayCashRegister(List<Strive.BusinessEntities.CashRegister.CashRegister> lstCashRegister);
        Result SaveCashRegisterNewApproach(List<Strive.BusinessEntities.CashRegister.CashRegisterList> lstCashRegisterConsolidate);
        
    }
}
