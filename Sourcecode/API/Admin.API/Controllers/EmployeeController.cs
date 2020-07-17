using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using Strive.BusinessEntities.Employee;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.Api.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class EmployeeController : ControllerBase
    {
        IEmployeeBpl _employeeBpl = null;

        public EmployeeController(IEmployeeBpl employeeBpl)
        {
            _employeeBpl = employeeBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllEmployee()
        {
            return _employeeBpl.GetEmployeeDetails();
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public Result GetAllEmployeeRoles()
        {
            return _employeeBpl.GetAllEmployeeRoles();
        }

        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public Result GetEmployeeById(long id)
        {
            return _employeeBpl.GetEmployeeByIdDetails(id);
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveEmployee([FromBody] EmployeeView lstEmployee)
        {
            return _employeeBpl.SaveEmployeeDetails(lstEmployee);
        }

        [HttpDelete]
        [Route("{id}")]
        public Result DeleteEmployee(long empId)
        {
            return _employeeBpl.DeleteEmployeeDetails(empId);
        }

    }
}