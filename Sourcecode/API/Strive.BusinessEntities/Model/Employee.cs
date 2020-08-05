using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblEmployee")]
public class Employee
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int EmployeeId { get; set; }

	[Column]
	public string FirstName { get; set; }

	[Column]
	public string MiddleName { get; set; }

	[Column]
	public string LastName { get; set; }

	[Column]
	public int? Gender { get; set; }

	[Column]
	public string SSNo { get; set; }

	[Column]
	public int? MaritalStatus { get; set; }

	[Column]
	public bool? IsCitizen { get; set; }

	[Column]
	public string AlienNo { get; set; }

	[Column]
	public DateTime? BirthDate { get; set; }

	[Column]
	public int? ImmigrationStatus { get; set; }

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