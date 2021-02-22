using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Employee;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Document;
using Strive.Common;
using Strive.Crypto;
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


        public Result AddEmployee(EmployeeModel employee)
        {

            bool success = false;

            //Validate emailId already exist
            var isExist = new CommonRal(_tenant, true).GetEmailIdExist(employee.EmployeeAddress.Email);

            if (isExist)
            {
                _result = Helper.BindValidationErrorResult("EmailId already exists. Try again.!!!");
                return _result;
            }

            //Validate documents

            var docBpl = new DocumentBpl(_cache, _tenant);

            if (employee.EmployeeDocument.Count > 0)
            {

                string error = docBpl.ValidateEmployeeFiles(employee.EmployeeDocument);
                if (!(error == string.Empty))
                {
                    _result = Helper.ErrorMessageResult(error);
                }

                return _result;
            }

            var commonBpl = new CommonBpl(_cache, _tenant);
            var createLogin = commonBpl.CreateLogin(UserType.Employee, employee.EmployeeAddress.Email, employee.EmployeeAddress.PhoneNumber);
            employee.EmployeeDetail.AuthId = createLogin.authId;

            if (createLogin.authId > 0)
            {
                if (employee.EmployeeDocument.Count > 0)
                {
                    //Documents Upload & Get File Names
                    List<EmployeeDocument> employeeDocument = docBpl.UploadEmployeeFiles(employee.EmployeeDocument);

                    employee.EmployeeDocument = employeeDocument;
                }

                //Add Employee
                var employeeResult = new EmployeeRal(_tenant).AddEmployee(employee);

                if (employeeResult)
                {
                    //Send Email

                    Dictionary<string, string> keyValues = new Dictionary<string, string>();
                    keyValues.Add("{{emailId}}", employee.EmployeeAddress.Email);
                    keyValues.Add("{{password}}", createLogin.password);
                    commonBpl.SendEmail(HtmlTemplate.ClientSignUp, employee.EmployeeAddress.Email, keyValues);

                   // commonBpl.SendLoginCreationEmail(HtmlTemplate.ClientSignUp, employee.EmployeeAddress.Email, createLogin.password);
                    success = true;
                }
                else if(createLogin.authId > 0)
                {
                    //Delete AuthMaster record from AuthDatabase
                    commonBpl.DeleteUser(createLogin.authId);
                }
                
            }

            return ResultWrap(success, "Status");
        }

        public Result UpdateEmployee(EmployeeModel employee)
        {
            return ResultWrap(new EmployeeRal(_tenant).UpdateEmployee, employee, "Status");
        }

        public Result DeleteEmployeeDetails(int employeeId)
        {
            try
            {
                return ResultWrap(new EmployeeRal(_tenant).DeleteEmployeeDetails, employeeId, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result GetEmployeeList()
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmployeeList, "EmployeeList");
        }

        public Result GetAllEmployeeDetail(SearchDto searchDto)
        {
            return ResultWrap(new EmployeeRal(_tenant).GetAllEmployeeDetail, searchDto, "EmployeeList");
        }

        public Result GetEmployeeById(int employeeId)
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmployeeById, employeeId, "Employee");
        }

        public Result GetAllEmployeeRoles()
        {
            return ResultWrap(new EmployeeRal(_tenant).GetAllEmployeeRoles, "EmployeeRoles");
        }

        public Result GetEmployeeRoleById(int employeeId)
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmployeeRoleById, employeeId, "EmployeeRole");
        }
    }
}
