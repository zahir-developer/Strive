using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.CashRegister.DTO;
using Strive.BusinessEntities.DTO;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Strive.BusinessLogic.CashRegister
{
    public class CashRegisterBpl : Strivebase, ICashRegisterBpl
    {
        public CashRegisterBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result SaveCashRegister(CashRegisterDto cashRegister)
        {
            CashRegisterDto CashInRegister = new CashRegisterRal(_tenant).GetCashRegisterDetails(CashRegisterType.CASHIN.ToString(), Convert.ToInt32(cashRegister.CashRegister.LocationId), cashRegister.CashRegister.CashRegisterDate ?? DateTime.Now);
            if (CashInRegister.CashRegister.CashRegisterId == cashRegister.CashRegister.CashRegisterId)
            {
                //Both are CashInRegister records, so no need to update store close date and close status
                return ResultWrap(new CashRegisterRal(_tenant).SaveCashRegister, cashRegister, "Status");
            }
            else
            {
                //Here, it is update for closed out register. So update the store close date in cashin register.
                //Get the status for store close.
                List<BusinessEntities.Code.Code> StoreSts = new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.StoreStatus);
                BusinessEntities.Code.Code ClosedSts = StoreSts.Where(x => x.CodeValue == "Closed")?.FirstOrDefault();
                CashInRegister.CashRegister.StoreOpenCloseStatus = ClosedSts?.CodeId;
                CashInRegister.CashRegister.StoreTimeOut = cashRegister.CashRegister.UpdatedDate?.DateTime;
                
                cashRegister.CashRegisterIn = CashInRegister.CashRegister;


                return ResultWrap(new CashRegisterRal(_tenant).SaveCashRegister, cashRegister, "Status");
            }
            
        }

        public Result GetCashRegisterDetails(CashRegisterType cashRegType, int locationId, DateTime cashRegDate)
        {
            return ResultWrap(new CashRegisterRal(_tenant).GetCashRegisterDetails, cashRegType.ToString(), locationId, cashRegDate, "CashRegister");
        }
        public Result GetCloseOutRegisterDetails(CashRegisterType cashRegType, int locationId, DateTime cashRegDate)
        {
            return ResultWrap(new CashRegisterRal(_tenant).GetCloseOutRegisterDetails, cashRegType.ToString(), locationId, cashRegDate, "CashRegister");
        }
        public Result GetTipDetail(TipdetailDto tipDetailDto)
        {
            return ResultWrap(new CashRegisterRal(_tenant).GetTipDetail, tipDetailDto, "CashRegister");
        }
    }
}
