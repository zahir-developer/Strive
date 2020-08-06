using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic;
using Strive.Common;


namespace Admin.Api.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]
    [Route("Admin/[Controller]")]
    public class EmployeeController : StriveControllerBase<IEmployeeBpl>
    {
        public EmployeeController(IEmployeeBpl empBpl) : base(empBpl) { }

        #region POST

        [HttpPost]
        [Route("Save")]
        public Result SaveEmployee([FromBody] EmployeeModel lstEmployee) => _bplManager.SaveEmployeeDetails(lstEmployee);

        [HttpPost]
        [Route("Delete")]
        public Result DeleteEmployee(int empId) => _bplManager.DeleteEmployeeDetails(empId);

        #endregion

        #region GET
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllEmployee() => _bplManager.GetEmployeeList();

        [HttpGet]
        [Route("GetAllRoles")]
        public Result GetAllEmployeeRoles() => _bplManager.GetAllEmployeeRoles();

        [HttpGet]
        [Route("GetEmployeeById")]
        public Result GetEmployeeById(int id) => _bplManager.GetEmployeeById(id); 
        #endregion
    }
}