using System;
using System.Collections.Generic;
using System.Text;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Employee;
using Strive.Common;

namespace Strive.BusinessLogic
{
    public interface IEmployeeBpl
    {
        Result GetEmployeeDetails();
        Result SaveEmployeeDetails(List<Employee> lstEmployee);
    }
}
