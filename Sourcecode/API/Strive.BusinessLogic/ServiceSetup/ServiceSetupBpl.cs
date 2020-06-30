using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.ServiceSetup;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic.ServiceSetup
{
    public class ServiceSetupBpl : Strivebase, IServiceSetupBpl
    {
        ITenantHelper tenant;
        JObject resultContent = new JObject();
        Result result;
        public ServiceSetupBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            tenant = tenantHelper;
        }
        public Result GetServiceSetupDetails()
        {
            try
            {
                var lstServiceSetup = new ServiceSetupRal(tenant).GetServiceSetupDetails();
                resultContent.Add(lstServiceSetup.WithName("Service Setup"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }
        public Result GetAllServiceType()
        {
            try
            {
                var lstServiceSetup = new ServiceSetupRal(tenant).GetAllServiceType();
                resultContent.Add(lstServiceSetup.WithName("Service Type"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }
        public Result GetServiceSetupById(int id)
        {
            try
            {
                var lstServiceSetup = new ServiceSetupRal(tenant).GetServiceSetupDetails();
                resultContent.Add(lstServiceSetup.WithName("Service Setup By Id"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }
        public Result SaveNewServiceDetails(List<tblService> lstServiceSetup)
        {
            try
            {
                bool blnStatus = new ServiceSetupRal(tenant).SaveNewServiceDetails(lstServiceSetup);
                resultContent.Add(blnStatus.WithName("Status"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }
        public Result DeleteServiceById(int id)
        {
            try
            {
                var lstServiceSetup = new ServiceSetupRal(tenant).DeleteServiceById(id);
                resultContent.Add(lstServiceSetup.WithName("Location"));
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