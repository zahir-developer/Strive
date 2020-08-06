using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.DTO.Employee;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Strive.BusinessLogic
{
    public class EmployeeBpl : Strivebase, IEmployeeBpl
    {
        public EmployeeBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }


        public Result GetEmployeeList()
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmployeeList, "EmployeeList");
        }

        public Result GetEmployeeById(int employeeId)
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmployeeById, employeeId, "Employee");
        }

        public Result GetAllEmployeeRoles()
        {
            try
            {
                var lstEmployeeRoles = new EmployeeRal(_tenant).GetAllEmployeeRoles();
                _resultContent.Add(lstEmployeeRoles.WithName("EmployeeRoles"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result SaveEmployeeDetails(EmployeeModel employee)
        {
            try
            {

                int authId = new CommonBpl(_cache, _tenant).CreateLogin(employee.EmployeeAddress.Email, employee.EmployeeAddress.PhoneNumber);

                employee.EmployeeDetail.AuthId = authId;

                var blnStatus = new EmployeeRal(_tenant).SaveEmployeeDetails(employee);

                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteEmployeeDetails(int empId)
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
