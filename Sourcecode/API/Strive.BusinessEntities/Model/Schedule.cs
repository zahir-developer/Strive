using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblSchedule")]
public class Schedule
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int ScheduleId { get; set; }

	[Column]
	public int? EmployeeId { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public int? RoleId { get; set; }

	[Column]
	public DateTime? ScheduledDate { get; set; }

	[Column]
	public DateTimeOffset? StartTime { get; set; }

	[Column]
	public DateTimeOffset? EndTime { get; set; }

	[Column]
	public int? ScheduleType { get; set; }

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