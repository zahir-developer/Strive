using System.Collections.Generic;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Model;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IEmployeeBpl
    {
        Result GetAllEmployeeRoles();
        Result SaveEmployeeDetails(EmployeeModel lstEmployee);
        Result DeleteEmployeeDetails(int empId);
        Result GetEmployeeById(int id);
        Result GetEmployeeList();
    }
}
