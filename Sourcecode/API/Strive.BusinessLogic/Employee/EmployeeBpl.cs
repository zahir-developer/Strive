using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.DTO.Employee;
using Strive.BusinessEntities.Employee;
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
        public Result SaveEmployeeDetails(EmployeeView employee)
        {
            try
            {
                var empDetails = employee.EmployeeDetail.FirstOrDefault();
                UserLogin lstEmployeelst = new UserLogin();
                lstEmployeelst.AuthId = 0;
                lstEmployeelst.EmailId = employee.EmployeeAddress.Select(a => a.Email).FirstOrDefault();
                lstEmployeelst.MobileNumber = employee.EmployeeAddress.Select(a => a.PhoneNumber).FirstOrDefault();
                lstEmployeelst.PasswordHash = "";
                lstEmployeelst.CreatedDate = employee.CreatedDate;
                var newitem = new CommonBpl(_cache, _tenant).CreateLogin(lstEmployeelst);


                lstEmployeelst.AuthId = newitem;
                empDetails.AuthId = newitem.toInt();

                var blnStatus = new EmployeeRal(_tenant).SaveEmployeeDetails(employee);


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
