﻿using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.Auth;
using Strive.BusinessEntities.DTO.Employee;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic.Common;
using Strive.BusinessLogic.Document;
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


        public Result AddEmployee(EmployeeModel employee)
        {
            var isExist = new EmployeeRal(_tenant).GetEmailIdExist(employee.EmployeeAddress.Email);

            if (isExist)
            {
                _result = Helper.BindValidationErrorResult("EmailId already exists. Try again.!!!");
                return _result;
            }

            int authId = new CommonBpl(_cache, _tenant).CreateLogin(employee.EmployeeAddress.Email, employee.EmployeeAddress.PhoneNumber);
            employee.EmployeeDetail.AuthId = authId;

            var docBpl = new DocumentBpl(_cache, _tenant);

            string error = docBpl.ValidateFiles(employee.EmployeeDocument);
            if (!(error == string.Empty))
            {
                _result = Helper.ErrorMessageResult(error);
            }

            //Documents Upload & Get File Names
            List<EmployeeDocument> employeeDocument = docBpl.UploadFiles(employee.EmployeeDocument);

            employee.EmployeeDocument = employeeDocument;

            return ResultWrap(new EmployeeRal(_tenant).AddEmployee, employee, "Status");
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

        public Result GetEmployeeById(int employeeId)
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmployeeById, employeeId, "Employee");
        }
        public Result GetEmailIdExist(string email)
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmailIdExist, email, "EmailIdExist");
        }

        public Result GetAllEmployeeRoles()
        {
            return ResultWrap(new EmployeeRal(_tenant).GetAllEmployeeRoles, "EmployeeRoles");
        }
        public Result GetEmployeeSearch(string employeeName)
        {
            return ResultWrap(new EmployeeRal(_tenant).GetEmployeeSearch, employeeName, "EmployeeList");
        }

    }
}
