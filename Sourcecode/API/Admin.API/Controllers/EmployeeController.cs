using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.Employee;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessLogic;
using Strive.Common;


namespace Admin.Api.Controllers
{
    [Authorize]    //[AutoValidateAntiforgeryToken]
    [Route("Admin/[Controller]")]
    public class EmployeeController : StriveControllerBase<IEmployeeBpl>
    {
        public EmployeeController(IEmployeeBpl empBpl) : base(empBpl) { }

        #region POST

        [HttpPost]
        [Route("Add")]
        public Result AddEmployee([FromBody] EmployeeModel employee) => _bplManager.AddEmployee(employee);

        [HttpPost]
        [Route("Update")]
        public Result UpdateEmployee([FromBody] EmployeeModel employee) => _bplManager.UpdateEmployee(employee);

        [HttpDelete]
        [Route("Delete/{employeeId}")]
        public Result DeleteEmployee(int employeeId) => _bplManager.DeleteEmployeeDetails(employeeId);

        #endregion

        #region GET
        [HttpGet]
        [Route("GetAllEmployeeDetail/{employeeName}")]
        public Result GetAllEmployee(string employeeName) => _bplManager.GetAllEmployeeDetail(employeeName);

        [HttpGet]
        [Route("GetAllRoles")]
        public Result GetAllEmployeeRoles() => _bplManager.GetAllEmployeeRoles();

        [HttpGet]
        [Route("GetEmployeeById")]
        public Result GetEmployeeById(int id) => _bplManager.GetEmployeeById(id);

        [HttpGet]
        [Route("GetAllEmplloyeeList")]
        public Result GetAllEmployeeList() => _bplManager.GetEmployeeList();

        [HttpGet]
        [Route("GetEmployeeRoleById")]
        public Result GetEmployeeRoleById(int id) => _bplManager.GetEmployeeRoleById(id);


        #endregion

    }
}