using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic.Common
{
    public class CommonBpl : Strivebase
    {
        ITenantHelper tenant;
        JObject resultContent = new JObject();
        Result result;
        public CommonBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            tenant = tenantHelper;
        }
        public Result GetAllCodes()
        {
            try
            {
                var lstCode = new CommonRal(tenant).GetAllCodes();
                resultContent.Add(lstCode.WithName("Codes"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }

        public Result GetCodesByCategory(GlobalCodes CodeCategory)
        {
            try
            {
                var lstCode = new CommonRal(tenant).GetCodeByCategory(CodeCategory);
                resultContent.Add(lstCode.WithName("Codes"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }

        public Result GetCodesByCategory(int CodeCategoryId)
        {
            try
            {
                var lstCode = new CommonRal(tenant).GetCodeByCategoryId(CodeCategoryId);
                resultContent.Add(lstCode.WithName("Codes"));
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
