using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblSchedule")]
public class Schedule
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int Id { get; set; }

	[Column]
	public int? UserId { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public int? RoleId { get; set; }

	[Column]
	public DateTimeOffset? ScheduledDate { get; set; }

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