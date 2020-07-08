using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using Strive.BusinessEntities.Employee;

namespace Strive.BusinessLogic
{
    public class EmployeeBpl : Strivebase, IEmployeeBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public EmployeeBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }
        public Result GetEmployeeDetails()
        {
            try
            {
                var lstEmployee = new EmployeeRal(_tenant).GetEmployeeDetails(); 
                _resultContent.Add(lstEmployee.WithName("Employee"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result SaveEmployeeDetails(List<Employee> lstEmployee)
        {
            try
            {
                bool blnStatus = new EmployeeRal(_tenant).SaveEmployeeDetails(lstEmployee);
                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteLocationDetails(long empId)
        {
            try
            {
                var lstEmployee = new EmployeeRal(_tenant).DeleteEmployeeDetails(empId);
                _resultContent.Add(lstEmployee.WithName("Employee"));
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
