using Strive.BusinessEntities.DTO;
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
        Result GetAllEmployeeDetail(SearchDto searchDto);
        Result GetEmployeeRoleById(int id);
        Result GetAllEmployeeName(int id);

        Result GetEmployeeHourlyRateById(int employeeId);
        Result GetEmployeePayCheck(EmployeePayCheckDto searchDto);
        Result SendEmployeeEmail();

    }
}
