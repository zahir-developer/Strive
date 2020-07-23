using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblJobDetail")]
public class JobDetail
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int JobDetailId { get; set; }

	[Column]
	public int? JobId { get; set; }

	[Column]
	public int? BayId { get; set; }

	[Column]
	public int? SalesRep { get; set; }

	[Column]
	public int? QABy { get; set; }

	[Column]
	public int? Labour { get; set; }

	[Column]
	public int? Review { get; set; }

	[Column]
	public string ReviewNote { get; set; }

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