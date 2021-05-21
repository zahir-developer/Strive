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



                if (employeeResult > 0)
                {
                    var employeeLocationRole = new EmployeeRal(_tenant).GetEmployeeById(employeeResult);
                    {

                        //Send Email to employee
                        string subject = " Welcome to Strive !";
                        Dictionary<string, string> keyValues = new Dictionary<string, string>();

                        keyValues.Add("{{employeeName}}", employee.Employee.FirstName);
                        string emplocation = string.Empty;
                        foreach (var empLoc in employeeLocationRole.EmployeeLocations)
                        {
                            emplocation += empLoc.LocationName + ", ";
                        }
                        char[] chars = { ',' };
                        emplocation = emplocation.TrimEnd(chars);
                        keyValues.Add("{{locationList}}", emplocation);
                        string emprole = string.Empty;
                        foreach (var empRole in employeeLocationRole.EmployeeRoles)
                        {
                            emprole += empRole.RoleName + ",";
                        }
                        char[] comma = { ',' };
                        emprole = emprole.TrimEnd(comma);
                        keyValues.Add("{{roleList}}", emprole);
                        keyValues.Add("{{emailId}}", employee.EmployeeAddress.Email);
                        keyValues.Add("{{password}}", createLogin.password);
                        keyValues.Add("{{url}}", _tenant.ApplicationUrl);
                        keyValues.Add("{{appUrl}}", _tenant.MobileUrl);
                        commonBpl.SendEmail(HtmlTemplate.EmployeeSignUp, employee.EmployeeAddress.Email, keyValues, subject);
                    }

                    //Send Email to Manager
                    {

                        char[] charToTrim = { ',' };
                        string id = string.Empty;
                        foreach (var item in employee.EmployeeLocation)
                        {
                            id += item.LocationId + ",";
                        }
                        
                        id = id.TrimEnd(charToTrim);
                        var emailId = new CommonRal(_tenant).GetEmailIdByRole(id);

                        string emailList = string.Empty;
                        foreach (var email in emailId)
                        {
                            emailList += email.Email + ",";
                        }
                        emailList = emailList.TrimEnd(charToTrim);
                        string sub = "New Employee Info!";
                        Dictionary<string, string> keyValues1 = new Dictionary<string, string>();
                        //keyValues1.Add("{{Manager/Operator}}", email.FirstName);
                        keyValues1.Add("{{employeeName}}", employee.Employee.FirstName);
                        string location = string.Empty;
                        foreach (var empLoc in employeeLocationRole.EmployeeLocations)
                        {
                            location += empLoc.LocationName + ",";
                        }
                       
                        location = location.TrimEnd(charToTrim);
                        keyValues1.Add("{{locationList}}", location);
                        string role = string.Empty;
                        foreach (var empRole in employeeLocationRole.EmployeeRoles)
                        {
                            role += empRole.RoleName + ",";
                        }
                        role = role.TrimEnd(charToTrim);
                        keyValues1.Add("{{roleList}}", role);
                        commonBpl.SendEmail(HtmlTemplate.NewEmployeeInfo, emailList, keyValues1, sub);
                    }

                


                success = true;

            }

            else if (createLogin.authId > 0)
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
    public Result GetAllEmployeeName(int id)
    {
        return ResultWrap(new EmployeeRal(_tenant).GetAllEmployeeName, id, "EmployeeName");
    }
    public Result GetEmployeeHourlyRateById(int employeeId)
    {
        return ResultWrap(new EmployeeRal(_tenant).GetEmployeeHourlyRateById, employeeId, "Employee");
    }

}
}
