using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblEmployeeLiabilityDetail")]
public class EmployeeLiabilityDetail
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int LiabilityDetailId { get; set; }

	[Column]
	public int? LiabilityId { get; set; }

	[Column]
	public int LiabilityDetailType { get; set; }

	[Column]
	public Double? Amount { get; set; }

	[Column]
	public int? PaymentType { get; set; }

	[Column]
	public string DocumentPath { get; set; }

	[Column]
	public string Description { get; set; }

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