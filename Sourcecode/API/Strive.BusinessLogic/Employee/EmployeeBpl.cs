using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Auth;
using System.Linq;
using Strive.BusinessLogic.Common;

namespace Strive.BusinessLogic
{
    public class EmployeeBpl : Strivebase, IEmployeeBpl
    {
        readonly ITenantHelper _tenant;
        readonly IDistributedCache _cache;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public EmployeeBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
            _cache = cache;
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
        public Result GetEmployeeByIdDetails(long id)
        {
            try
            {
                var lstEmpDetailById = new EmployeeRal(_tenant).GetEmployeeByIdDetails(id);
                _resultContent.Add(lstEmpDetailById.WithName("EmployeeDetail"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result SaveEmployeeDetails(EmployeeView lstEmployee)
        {
            try
            {

                UserLogin lstEmployeelst = new UserLogin();
                lstEmployeelst.AuthId = 0;
                lstEmployeelst.EmailId = lstEmployee.EmployeeAddress.Select(a => a.Email).FirstOrDefault();
                lstEmployeelst.MobileNumber = lstEmployee.EmployeeAddress.Select(a => a.PhoneNumber).FirstOrDefault();
                lstEmployeelst.PasswordHash = "";
                lstEmployeelst.CreatedDate = lstEmployee.CreatedDate;
                var newitem = new CommonBpl(_cache, _tenant).CreateLogin(lstEmployeelst);
                var blnStatus = new EmployeeRal(_tenant).SaveEmployeeDetails(lstEmployee);

                //if (blnStatus)
                //{
                //    List<EmployeeLogin> lstEmployeeLogin = new List<EmployeeLogin>();
                //    lstEmployeeLogin.Add(new EmployeeLogin());
                //    new EmployeeRal(_tenant, true).SaveEmployeeLogin(lstEmployeeLogin);
                //}

                _resultContent.Add(blnStatus.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteEmployeeDetails(long empId)
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
