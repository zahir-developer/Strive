using Strive.BusinessEntities;
using Strive.BusinessEntities.CashRegister.DTO;
using Strive.Common;
using System;

namespace Strive.ResourceAccess
{
    public class CashRegisterRal : RalBase
    {
        public CashRegisterRal(ITenantHelper tenant) : base(tenant) { }

        public bool SaveCashRegister(CashRegisterDto cashRegister)
        {
            return dbRepo.SavePc(cashRegister, "CashRegisterId");
        }

        public CashRegisterDto GetCashRegisterDetails(string cashRegisterType, int locationId, DateTime dateTime)
        {
            _prm.Add("@LocationId", locationId);
            _prm.Add("@CashRegisterType", cashRegisterType);
            _prm.Add("@CashRegisterDate", dateTime.ToString("yyy-MM-dd"));
            var result = db.FetchMultiResult<CashRegisterDto>(SPEnum.USPGETCASHREGISTER.ToString(), _prm);
            return result;
        }
    }
}
