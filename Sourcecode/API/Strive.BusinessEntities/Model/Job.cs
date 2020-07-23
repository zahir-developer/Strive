using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblJob")]
public class Job
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int JobId { get; set; }

	[Column]
	public string TicketNumber { get; set; }

	[Column]
	public string BarCode { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public int? ClientId { get; set; }

	[Column]
	public int? JobType { get; set; }

	[Column]
	public int? VehicleId { get; set; }

	[Column]
	public DateTimeOffset? TimeIn { get; set; }

	[Column]
	public DateTimeOffset? EstimatedTimeOut { get; set; }

	[Column]
	public DateTimeOffset? ActualTimeOut { get; set; }

	[Column]
	public int? JobStatus { get; set; }

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