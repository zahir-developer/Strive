using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.CashRegister.DTO;
using Strive.BusinessEntities.Code;
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
            CashRegisterDto CashInRegister = new CashRegisterRal(_tenant).GetCashRegisterDetails(CashRegisterType.CASHIN.ToString(), (int)cashRegister.CashRegister.LocationId, cashRegister.CashRegister.CashRegisterDate ?? DateTime.Now);
            
            if (CashInRegister.CashRegister == null)
            {
                List<Code> cashRegisterType = new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.CASHREGISTERTYPE);
                CashInRegister.CashRegister = new BusinessEntities.Model.CashRegister();
                CashInRegister.CashRegister.CashRegisterType = cashRegisterType.Where(x => x.CodeValue.ToUpper() == "CASHIN")?.FirstOrDefault()?.CodeId;

                CashInRegister.CashRegister.DrawerId = cashRegister.CashRegister.DrawerId;
                CashInRegister.CashRegister.LocationId = cashRegister.CashRegister.LocationId;
                CashInRegister.CashRegister.CashRegisterDate = cashRegister.CashRegister.CashRegisterDate;
                CashInRegister.CashRegister.IsActive = cashRegister.CashRegister.IsActive;
                CashInRegister.CashRegister.IsDeleted = cashRegister.CashRegister.IsDeleted;
                CashInRegister.CashRegister.CreatedDate = cashRegister.CashRegister.CreatedDate;
                CashInRegister.CashRegister.UpdatedDate = cashRegister.CashRegister.UpdatedDate;
                CashInRegister.CashRegister.CreatedBy = cashRegister.CashRegister.CreatedBy;
                CashInRegister.CashRegister.UpdatedBy = cashRegister.CashRegister.UpdatedBy;
                CashInRegister.CashRegister.CashRegisterDate = cashRegister.CashRegister.CashRegisterDate;

                CashInRegister.CashRegisterCoins = new BusinessEntities.Model.CashRegisterCoins();
                CashInRegister.CashRegisterCoins.IsActive = cashRegister.CashRegister.IsActive;
                CashInRegister.CashRegisterCoins.CreatedDate = cashRegister.CashRegister.CreatedDate;
                CashInRegister.CashRegisterCoins.UpdatedDate = cashRegister.CashRegister.UpdatedDate;
                CashInRegister.CashRegisterCoins.CreatedBy = cashRegister.CashRegister.CreatedBy;
                CashInRegister.CashRegisterCoins.UpdatedBy = cashRegister.CashRegister.UpdatedBy;
                CashInRegister.CashRegisterCoins.IsDeleted = cashRegister.CashRegister.IsDeleted;

                CashInRegister.CashRegisterRolls = new BusinessEntities.Model.CashRegisterRolls();
                CashInRegister.CashRegisterRolls.IsActive = cashRegister.CashRegister.IsActive;
                CashInRegister.CashRegisterRolls.CreatedDate = cashRegister.CashRegister.CreatedDate;
                CashInRegister.CashRegisterRolls.UpdatedDate = cashRegister.CashRegister.UpdatedDate;
                CashInRegister.CashRegisterRolls.CreatedBy = cashRegister.CashRegister.CreatedBy;
                CashInRegister.CashRegisterRolls.UpdatedBy = cashRegister.CashRegister.UpdatedBy;
                CashInRegister.CashRegisterRolls.IsDeleted = cashRegister.CashRegister.IsDeleted;

                CashInRegister.CashRegisterBills = new BusinessEntities.Model.CashRegisterBills();
                CashInRegister.CashRegisterBills.IsActive = cashRegister.CashRegister.IsActive;
                CashInRegister.CashRegisterBills.CreatedDate = cashRegister.CashRegister.CreatedDate;
                CashInRegister.CashRegisterBills.UpdatedDate = cashRegister.CashRegister.UpdatedDate;
                CashInRegister.CashRegisterBills.CreatedBy = cashRegister.CashRegister.CreatedBy;
                CashInRegister.CashRegisterBills.UpdatedBy = cashRegister.CashRegister.UpdatedBy;
                CashInRegister.CashRegisterBills.IsDeleted = cashRegister.CashRegister.IsDeleted;

                CashInRegister.CashRegisterOthers = new BusinessEntities.Model.CashRegisterOthers();
                CashInRegister.CashRegisterOthers.IsActive = cashRegister.CashRegister.IsActive;
                CashInRegister.CashRegisterOthers.CreatedDate = cashRegister.CashRegister.CreatedDate;
                CashInRegister.CashRegisterOthers.UpdatedDate = cashRegister.CashRegister.UpdatedDate;
                CashInRegister.CashRegisterOthers.CreatedBy = cashRegister.CashRegister.CreatedBy;
                CashInRegister.CashRegisterOthers.UpdatedBy = cashRegister.CashRegister.UpdatedBy;
                CashInRegister.CashRegisterOthers.IsDeleted = cashRegister.CashRegister.IsDeleted;

            }
            if (CashInRegister.CashRegister.CashRegisterType != cashRegister.CashRegister.CashRegisterType)
            {

                //Here, it is update for closed out register. So update the store close date in cashin register.
                //Get the status for store close.
                List<Code> StoreSts = new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.StoreStatus);
                BusinessEntities.Code.Code ClosedSts = StoreSts.Where(x => x.CodeValue == "Closed")?.FirstOrDefault();
                CashInRegister.CashRegister.StoreOpenCloseStatus = ClosedSts?.CodeId;
                CashInRegister.CashRegister.StoreTimeOut = cashRegister.CashRegister.UpdatedDate?.DateTime;

                //cashRegister.CashRegisterIn = CashInRegister.CashRegister;

                CashRegisterDto cashInRegister = new CashRegisterDto();
                cashInRegister.CashRegister = CashInRegister.CashRegister;
                cashInRegister.CashRegisterCoins = CashInRegister.CashRegisterCoins;
                cashInRegister.CashRegisterRolls = CashInRegister.CashRegisterRolls;
                cashInRegister.CashRegisterBills = CashInRegister.CashRegisterBills;
                cashInRegister.CashRegisterOthers = CashInRegister.CashRegisterOthers;
                ResultWrap(new CashRegisterRal(_tenant).SaveCashRegister, cashInRegister, "cashInRegister");


            }
            return ResultWrap(new CashRegisterRal(_tenant).SaveCashRegister, cashRegister, "Status");
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
