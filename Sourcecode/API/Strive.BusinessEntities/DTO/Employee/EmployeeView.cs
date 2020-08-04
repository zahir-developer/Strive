using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Strive.BusinessEntities.Employee
{
    public class EmployeeView : Employee
    {
        public List<EmployeeDetail> EmployeeDetail { get; set; }
        public List<EmployeeAddress> EmployeeAddress { get; set; }
        public List<EmployeeRole> EmployeeRole { get; set; }
    }
}
