using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Model
{
    public class EmployeeModel
    {
        public Model.Employee EmployeeInfo { get; set; }

        public EmployeeDetail EmployeeDetail { get; set; }
        public EmployeeLocation EmployeeLocation { get; set; }
        public EmployeeAddress EmployeeAddress { get; set; }
        public EmployeeDocument EmployeeDocument { get; set; }
        public EmployeeLiability EmployeeCollision { get; set; }
        public EmployeeRole EmployeeRoles { get; set; }
    }
}
