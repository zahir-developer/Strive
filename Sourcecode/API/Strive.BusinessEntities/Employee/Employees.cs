using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.Employee
{
    public class Employees 
    {
        public List<EmployeeInformation> Employee { get; set; }
        public List<EmployeeDetail> EmployeeDetail { get; set; }
        public List<EmployeeAddress> EmployeeAddress { get; set; }
    }
}
