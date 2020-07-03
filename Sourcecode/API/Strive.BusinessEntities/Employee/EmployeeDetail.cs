using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Strive.BusinessEntities.Employee
{
    public class EmployeeDetail
    {
        public int EmployeeDetailId { get; set; }
		public int EmployeeId { get; set; }
		[MaxLength(10)]
		public string EmployeeCode { get; set; }
		public int AuthId { get; set; }
		public int LocationId { get; set; }
		[MaxLength(10)]
		public string PayRate { get; set; }
		[MaxLength(10)]
		public string SickRate { get; set; }
		[MaxLength(10)]
		public string VacRate { get; set; }
		[MaxLength(10)]
		public string ComRate { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? HiredDate { get; set; }
		[MaxLength(10)]
		public string Salary { get; set; }
		[MaxLength(10)]
		public string Tip { get; set; }
		[Column(TypeName = "datetime")]
		public DateTime? LRT { get; set; }
		public short? Exemptions { get; set; }
	}
}
