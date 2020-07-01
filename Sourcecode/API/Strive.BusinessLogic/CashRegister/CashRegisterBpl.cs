using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.CashRegister;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic.CashRegister
{
    public class CashRegisterBpl : Strivebase, ICashRegisterBpl
    {
        ITenantHelper tenant;
        JObject resultContent = new JObject();
        Result result;
        public CashRegisterBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            tenant = tenantHelper;
        }
        
        public Result GetCashRegisterByDate(DateTime dateTime)
        {
            try
            {
                var lstCashRegisterConsolidate = new CashRegisterRal(tenant).GetCashRegisterByDate(dateTime);
                resultContent.Add(lstCashRegisterConsolidate.WithName("Cash Register By Current Date"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }
        public Result SaveTodayCashRegister(List<CashRegisterConsolidate> lstCashRegisterConsolidate)
        {
            try
            {
                bool blnStatus = new CashRegisterRal(tenant).SaveTodayCashRegister(lstCashRegisterConsolidate);
                resultContent.Add(blnStatus.WithName("Status"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }
       
    }
}
