using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblJobItem")]
public class JobItem
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int JobItemId { get; set; }

	[Column]
	public int? JobId { get; set; }

	[Column]
	public int? ServiceId { get; set; }

	[Column]
	public decimal? Commission { get; set; }

	[Column]
	public decimal? Price { get; set; }

	[Column]
	public int? Quantity { get; set; }

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