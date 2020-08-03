using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.CashRegister;
using Strive.BusinessEntities.CashRegister.DTO;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;

namespace Strive.BusinessLogic.CashRegister
{
    public class CashRegisterBpl : Strivebase, ICashRegisterBpl
    {
        public CashRegisterBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }

        public Result SaveCashRegister(CashRegisterDto cashRegister)
        {
            return ResultWrap(new CashRegisterRal(_tenant).SaveCashRegister, cashRegister, "Status");
        }

        public Result GetCashRegisterDetails(CashRegisterType cashRegType, int locationId, DateTime cashRegDate)
        {
            return ResultWrap(new CashRegisterRal(_tenant).GetCashRegisterDetails, cashRegType.ToString(), locationId, cashRegDate, "CashRegister");
        }
    }
}
