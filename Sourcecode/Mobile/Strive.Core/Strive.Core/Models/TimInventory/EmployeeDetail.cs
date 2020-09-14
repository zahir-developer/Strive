using System;
namespace Strive.Core.Models.TimInventory
{
    public class EmployeeDetail
    {
        public EmployeeDetail()
        {
        }

        public int EmployeeDetailId { get; set; }
        public object EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public object AuthId { get; set; }
        public object LocationId { get; set; }
        public object PayRate { get; set; }
        public object SickRate { get; set; }
        public object VacRate { get; set; }
        public object ComRate { get; set; }
        public object HiredDate { get; set; }
        public object Salary { get; set; }
        public object Tip { get; set; }
        public DateTime LRT { get; set; }
        public int Exemptions { get; set; }
        public bool IsActive { get; set; }
    }
}
