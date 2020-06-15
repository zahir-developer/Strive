using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;

namespace Strive.BusinessLogic
{
    public class EmployeeBpl : Strivebase, IEmployeeBpl
    {
        ITenantHelper tenant;
        JObject resultContent = new JObject();
        Result result;
        public EmployeeBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            tenant = tenantHelper;
        }
        public Result GetEmployeeDetails()
        {
            try
            {
                var lstEmployee = new EmployeeRal(tenant).GetEmployeeDetails();
                resultContent.Add(lstEmployee.WithName("Employee"));
                result = Helper.BindSuccessResult(resultContent);
            }
            catch (Exception ex)
            {
                result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return result;
        }

        public Result SaveEmployeeDetails(List<Employee> lstEmployee)
        {
            try
            {
                bool blnStatus = new EmployeeRal(tenant).SaveEmployeeDetails(lstEmployee);
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
