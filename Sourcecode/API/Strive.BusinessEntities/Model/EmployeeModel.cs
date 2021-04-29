
using System.Collections.Generic;

namespace Strive.BusinessEntities.Model
{
    public class EmployeeModel
    {
        public Model.Employee Employee { get; set; }
        public List<EmployeeLocation> EmployeeLocation { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public EmployeeAddress EmployeeAddress { get; set; }
        public List<EmployeeDocument> EmployeeDocument { get; set; }
        public List<EmployeeLiability> EmployeeLiability { get; set; }
        public List<EmployeeRole> EmployeeRole { get; set; }

        public List<EmployeeHourlyRate> EmployeeHourlyRate { get; set; }
    }
}
