using System.Collections.Generic;
using Strive.BusinessEntities.Employee;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IEmployeeBpl
    {
        Result GetEmployeeDetails();
        Result GetAllEmployeeRoles();
        Result SaveEmployeeDetails(EmployeeView lstEmployee);
        Result DeleteEmployeeDetails(long empId);
        Result GetEmployeeByIdDetails(long id);
    }
}
