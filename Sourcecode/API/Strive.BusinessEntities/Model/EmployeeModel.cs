
namespace Strive.BusinessEntities.Model
{
    public class EmployeeModel
    {
        public Model.Employee Employee { get; set; }
        public EmployeeLocation EmployeeLocation { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public EmployeeAddress EmployeeAddress { get; set; }
        public EmployeeDocument EmployeeDocument { get; set; }
        public EmployeeLiability EmployeeLiability { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
    }
}
