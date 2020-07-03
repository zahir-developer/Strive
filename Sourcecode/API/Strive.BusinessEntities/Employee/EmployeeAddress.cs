using System.ComponentModel.DataAnnotations;

namespace Strive.BusinessEntities.Employee
{
    public class EmployeeAddress
    {
		public int AddressId { get; set; }
		public int? RelationshipId { get; set; }
		[MaxLength(50)]
		public string Address1 { get; set; }
		[MaxLength(50)]
		public string Address2 { get; set; }
		[MaxLength(50)]
		public string PhoneNumber { get; set; }
		[MaxLength(50)]
		public string PhoneNumber2 { get; set; }
		[MaxLength(50)]
		public string Email { get; set; }
		public int? City { get; set; }
		public int? State { get; set; }
		[MaxLength(50)]
		public string Zip { get; set; }
		public bool? IsActive { get; set; }
	}
}
