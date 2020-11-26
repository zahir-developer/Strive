using Strive.BusinessEntities.CashRegister.DTO;
using Strive.Common;
using System;

namespace Strive.BusinessLogic.CashRegister
{
    public interface ICashRegisterBpl
    {
        Result SaveCashRegister(CashRegisterDto cashRegister);
        Result GetCashRegisterDetails(CashRegisterType cashRegisterType, int locationId, DateTime dateTime);
        Result GetCloseOutRegisterDetails(CashRegisterType cashRegisterType, int locationId, DateTime dateTime);
    }
}
