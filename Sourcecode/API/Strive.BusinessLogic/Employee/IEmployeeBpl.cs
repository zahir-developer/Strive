using Strive.BusinessEntities.DTO.Employee;
using Strive.BusinessEntities.Model;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IEmployeeBpl
    {
        Result GetAllEmployeeRoles();
        Result AddEmployee(EmployeeModel employee);
        Result UpdateEmployee(EmployeeModel employee);
        Result DeleteEmployeeDetails(int empId);
        Result GetEmployeeById(int id);
        Result GetEmployeeList();
        Result GetEmployeeSearch(string employeeName);
        Result GetEmailIdExist(string email);
    }
}
