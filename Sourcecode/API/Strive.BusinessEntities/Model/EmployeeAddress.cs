using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblEmployeeAddress")]
public class EmployeeAddress
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int EmployeeAddressId { get; set; }

	[Column]
	public int? EmployeeId { get; set; }

	[Column]
	public string Address1 { get; set; }

	[Column]
	public string Address2 { get; set; }

	[Column]
	public string PhoneNumber { get; set; }

	[Column]
	public string PhoneNumber2 { get; set; }

	[Column]
	public string Email { get; set; }

	[Column]
	public int? City { get; set; }

	[Column]
	public int? State { get; set; }

	[Column]
	public string Zip { get; set; }

	[Column]
	public int? Country { get; set; }

	[Column]
	public bool? IsActive { get; set; }

	[Column]
	public bool? IsDeleted { get; set; }

	[Column]
	public int? CreatedBy { get; set; }

	[Column]
	public DateTimeOffset? CreatedDate { get; set; }

	[Column]
	public int? UpdatedBy { get; set; }

	[Column]
	public DateTimeOffset? UpdatedDate { get; set; }

}
}