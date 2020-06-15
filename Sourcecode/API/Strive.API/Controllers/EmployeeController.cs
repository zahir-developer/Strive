using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace aHEAdWebAPI.Controllers
{
    [Authorize]
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

    }
}