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
using System.ComponentModel.DataAnnotations;
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

            if (employee != null)
            {

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

                    // return _result;
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
                    var employeeId = new EmployeeRal(_tenant).AddEmployee(employee);

                    if (employeeId > 0)
                    {
                        //Send Email to employee
                        SendEmployeeNotification(employee, employee.EmployeeLocation, employee.EmployeeRole, employeeId, createLogin.password);

                        //Send Email to Manager
                        SendEmailManagerNotification(employee, employee.EmployeeLocation, employee.EmployeeRole);

                        success = true;
                    }

                    else if (createLogin.authId > 0)
                    {
                        //Delete AuthMaster record from AuthDatabase
                        commonBpl.DeleteUser(createLogin.authId);
                    }

                }
            }
            else
                return Helper.BindValidationErrorResult("Internal Error.");

            return ResultWrap(success, "Status");
        }

        private void SendEmployeeNotification(EmployeeModel employee, List<EmployeeLocation> employeeLocations, List<Strive.BusinessEntities.Model.EmployeeRole> employeeRoles, int employeeId, string password)
        {
            if (employeeLocations != null)
            {
                var subject = EmailSubject.WelcomeEmail;
                Dictionary<string, string> keyValues = new Dictionary<string, string>();

                keyValues.Add("{{employeeName}}", employee.Employee.FirstName);
                string emplocation = string.Empty;
                foreach (var empLoc in employeeLocations)
                {
                    emplocation += empLoc.LocationName + ",";
                }
                char[] chars = { ',' };
                emplocation = emplocation.TrimEnd(chars);
                keyValues.Add("{{locationList}}", emplocation);
                string emprole = string.Empty;
                foreach (var empRole in employeeRoles)
                {
                    emprole += empRole.RoleName + ",";
                }
                char[] comma = { ',' };
                emprole = emprole.TrimEnd(comma);
                keyValues.Add("{{roleList}}", emprole);
                keyValues.Add("{{emailId}}", employee.EmployeeAddress.Email);
                keyValues.Add("{{password}}", password);
                keyValues.Add("{{url}}", _tenant.ApplicationUrl);
                keyValues.Add("{{appUrl}}", _tenant.MobileUrl);

                var commonBpl = new CommonBpl(_cache, _tenant);

                commonBpl.SendEmail(HtmlTemplate.EmployeeSignUp, employee.EmployeeAddress.Email, keyValues, subject);
            }
        }

        private void SendEmailManagerNotification(EmployeeModel employee, List<EmployeeLocation> employeeLocations, List<Strive.BusinessEntities.Model.EmployeeRole> employeeRoles)
        {

            char[] charToTrim = { ',' };
            string id = string.Empty;
            foreach (var item in employee.EmployeeLocation)
            {
                id += item.LocationId + ",";
            }

            id = id.TrimEnd(charToTrim);
            var emailId = new CommonRal(_tenant).GetEmailIdByRole(id, DateTime.Now, DateTime.Now);

            string emailList = string.Empty;
            foreach (var email in emailId)
            {
                emailList += email.Email + ",";
            }
            emailList = emailList.TrimEnd(charToTrim);
            var sub = EmailSubject.Manager;
            Dictionary<string, string> keyValues1 = new Dictionary<string, string>();
            //keyValues1.Add("{{Manager/Operator}}", email.FirstName);
            keyValues1.Add("{{employeeName}}", employee.Employee.FirstName);
            string location = string.Empty;
            foreach (var empLoc in employeeLocations)
            {
                location += empLoc.LocationName + ",";
            }

            location = location.TrimEnd(charToTrim);
            keyValues1.Add("{{locationList}}", location);
            string role = string.Empty;
            foreach (var empRole in employeeRoles)
            {
                role += empRole.RoleName + ",";
            }
            role = role.TrimEnd(charToTrim);
            keyValues1.Add("{{roleList}}", role);
            var commonBpl = new CommonBpl(_cache, _tenant);

            commonBpl.SendEmail(HtmlTemplate.NewEmployeeInfo, emailList, keyValues1, sub);
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
        public Result GetEmployeePayCheck(EmployeePayCheckDto checkDto)
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmployeePayCheck, checkDto, "EmployeePayCheck");
        }

        public bool SendEmployeeEmail()
        {
            var allEmployeeList = new EmployeeRal(_tenant).GetEmployeeList();
            foreach (var emp in allEmployeeList.Employee)
            {
                var empDetail = new EmployeeRal(_tenant).GetEmployeeById(emp.EmployeeId);
                if (!string.IsNullOrWhiteSpace(empDetail.EmployeeInfo.Email) && !empDetail.EmployeeInfo.IsNotified)
                {
                    var splitEmail = empDetail.EmployeeInfo.Email.Split(',');
                    foreach (var email in splitEmail)
                    {

                        if (new EmailAddressAttribute().IsValid(email))
                        {
                            SendEmployeeMail(empDetail.EmployeeInfo, email, empDetail.EmployeeLocations, empDetail.EmployeeRoles, emp.EmployeeId);
                            new EmployeeRal(_tenant).UpdateEmployeeAddressIsNotified(empDetail.EmployeeInfo.EmployeeAddressId, true);
                            return false;
                        }
                    }
                }

            }

            return true;
        }

        private void SendEmployeeMail(EmployeeInfoDto employee, string email, List<EmployeeLocationDto> employeeLocations, List<EmployeeRoleDto> employeeRoles, int employeeId)
        {
            var commonBpl = new CommonBpl(_cache, _tenant);
            if (employeeLocations != null)
            {
                var isExist = new CommonRal(_tenant, true).GetEmailIdExist(email);

                if (!isExist)
                {
                    var createLogin = commonBpl.CreateLogin(UserType.Employee, email, employee.PhoneNumber);
                    var subject = EmailSubject.WelcomeEmail;
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();

                    keyValues.Add("{{employeeName}}", employee.Firstname);
                    string emplocation = string.Empty;
                    foreach (var empLoc in employeeLocations)
                    {
                        emplocation += empLoc.LocationName + ",";
                    }
                    char[] chars = { ',' };
                    emplocation = emplocation.TrimEnd(chars);
                    keyValues.Add("{{locationList}}", emplocation);
                    string emprole = string.Empty;
                    foreach (var empRole in employeeRoles)
                    {
                        emprole += empRole.RoleName + ",";
                    }
                    char[] comma = { ',' };
                    emprole = emprole.TrimEnd(comma);
                    keyValues.Add("{{roleList}}", emprole);
                    keyValues.Add("{{emailId}}", email);
                    keyValues.Add("{{password}}", createLogin.password);
                    keyValues.Add("{{url}}", _tenant.ApplicationUrl);
                    keyValues.Add("{{appUrl}}", _tenant.MobileUrl);



                    commonBpl.SendEmail(HtmlTemplate.EmployeeSignUp, email, keyValues, subject);
                }
                else
                {
                    var createLogin = commonBpl.GetUserPassword(email, UserType.Employee);
                    var subject = EmailSubject.WelcomeEmail;
                    Dictionary<string, string> keyValues = new Dictionary<string, string>();

                    keyValues.Add("{{employeeName}}", employee.Firstname);
                    string emplocation = string.Empty;
                    foreach (var empLoc in employeeLocations)
                    {
                        emplocation += empLoc.LocationName + ",";
                    }
                    char[] chars = { ',' };
                    emplocation = emplocation.TrimEnd(chars);
                    keyValues.Add("{{locationList}}", emplocation);
                    string emprole = string.Empty;
                    foreach (var empRole in employeeRoles)
                    {
                        emprole += empRole.RoleName + ",";
                    }
                    char[] comma = { ',' };
                    emprole = emprole.TrimEnd(comma);
                    keyValues.Add("{{roleList}}", emprole);
                    keyValues.Add("{{emailId}}", email);
                    keyValues.Add("{{password}}", createLogin.Password);
                    keyValues.Add("{{url}}", _tenant.ApplicationUrl);
                    keyValues.Add("{{appUrl}}", _tenant.MobileUrl);



                    commonBpl.SendEmail(HtmlTemplate.EmployeeSignUp, email, keyValues, subject);
                }
            }
        }
    }
}
