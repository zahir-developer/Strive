using System.ComponentModel.DataAnnotations;

namespace Strive.BusinessEntities.Employee
{
    public class EmployeeAddressDto
    {
		public int? EmployeeAddressId { get; set; }
        public long RelationshipId { get; set; }
        public string Address1 { get; set; }
		public string Address2 { get; set; }
		public string PhoneNumber { get; set; }
		public string PhoneNumber2 { get; set; }
		public string Email { get; set; }
		public int City { get; set; }
		public int State { get; set; }
		public string Zip { get; set; }
		public bool IsActive { get; set; }
        public int Country { get; set; }
    }
}
