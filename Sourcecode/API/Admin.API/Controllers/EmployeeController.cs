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
        [Route("GetAll")]
        public Result GetAllEmployee() => _bplManager.GetEmployeeList();

        [HttpGet]
        [Route("GetAllRoles")]
        public Result GetAllEmployeeRoles() => _bplManager.GetAllEmployeeRoles();

        [HttpGet]
        [Route("GetEmployeeById")]
        public Result GetEmployeeById(int id) => _bplManager.GetEmployeeById(id);
        #endregion

        #region
        [HttpPost]
        [Route("GetEmployeeSearch")]
        public Result GetEmployeeSearch(string employeeName) => _bplManager.GetEmployeeSearch(employeeName);
        #endregion

        #region
        [HttpPost]
        [Route("GetEmailIdExist/{email}")]
        public Result GetEmailIdExist(string email) => _bplManager.GetEmailIdExist(email);
        #endregion
    }
}