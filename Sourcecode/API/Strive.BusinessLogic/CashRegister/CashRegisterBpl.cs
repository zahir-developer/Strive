using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.CashRegister;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;

namespace Strive.BusinessLogic.CashRegister
{
    public class CashRegisterBpl : Strivebase, ICashRegisterBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public CashRegisterBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }
        
        public Result GetCashRegisterByDate(DateTime dateTime)
        {
            try
            {
                var lstCashRegisterConsolidate = new CashRegisterRal(_tenant).GetCashRegisterByDate(dateTime);
                _resultContent.Add(lstCashRegisterConsolidate.WithName("CashRegister"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result SaveTodayCashRegister(List<Strive.BusinessEntities.CashRegister.CashRegister> lstCashRegisterConsolidate)
        {
            try
            {
                bool blnStatus = new CashRegisterRal(_tenant).SaveTodayCashRegister(lstCashRegisterConsolidate);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result SaveCashRegisterNewApproach(List<Strive.BusinessEntities.CashRegister.CashRegister> lstCashRegisterConsolidate)
        {
            try
            {
                bool blnStatus = new CashRegisterRal(_tenant).SaveCashRegisterNewApproach(lstCashRegisterConsolidate);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }


        

    }
}
