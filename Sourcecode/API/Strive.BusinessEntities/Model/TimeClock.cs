using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblTimeClock")]
public class TimeClock
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int TimeClockId { get; set; }

	[Column, PrimaryKey]
	public int EmployeeId { get; set; }

	[Column]
	public int LocationId { get; set; }

	[Column]
	public int? RoleId { get; set; }

	[Column]
	public DateTime? EventDate { get; set; }

	[Column]
	public DateTimeOffset? InTime { get; set; }

	[Column]
	public DateTimeOffset? OutTime { get; set; }

	[Column]
	public int? EventType { get; set; }

	[Column]
	public string UpdatedFrom { get; set; }

	[Column]
	public bool Status { get; set; }

	[Column]
	public string Comments { get; set; }

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