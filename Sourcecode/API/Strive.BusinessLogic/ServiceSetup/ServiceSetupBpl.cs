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
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public ServiceSetupBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }
        public Result GetServiceSetupDetails()
        {
            try
            {
                var lstServiceSetup = new ServiceSetupRal(_tenant).GetServiceSetupDetails();
                _resultContent.Add(lstServiceSetup.WithName("ServiceSetup"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetAllServiceType()
        {
            try
            {
                var lstServiceSetup = new ServiceSetupRal(_tenant).GetAllServiceType();
                _resultContent.Add(lstServiceSetup.WithName("ServiceType"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetServiceSetupById(int id)
        {
            try
            {
                var lstServiceSetup = new ServiceSetupRal(_tenant).GetServiceSetupById(id);
                _resultContent.Add(lstServiceSetup.WithName("ServiceSetupById"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result SaveNewServiceDetails(List<tblService> lstServiceSetup)
        {
            try
            {
                bool blnStatus = new ServiceSetupRal(_tenant).SaveNewServiceDetails(lstServiceSetup);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteServiceById(int id)
        {
            try
            {
                var lstServiceSetup = new ServiceSetupRal(_tenant).DeleteServiceById(id);
                _resultContent.Add(lstServiceSetup.WithName("Location"));
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