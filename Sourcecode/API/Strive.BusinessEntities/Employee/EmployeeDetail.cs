using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strive.BusinessEntities.Employee
{
    public class EmployeeDetail
    {
        public int? EmployeeDetailId { get; set; }
		public int? EmployeeId { get; set; }
		public string EmployeeCode { get; set; }
		public int? AuthId { get; set; }
		public int? LocationId { get; set; }
		public string PayRate { get; set; }
		public string SickRate { get; set; }
		public string VacRate { get; set; }
		public string ComRate { get; set; }
		public DateTime? HiredDate { get; set; }
		public string Salary { get; set; }
		public string Tip { get; set; }
		public DateTime LRT { get; set; }
		public short Exemptions { get; set; }
        public bool IsActive { get; set; }
	}
}
