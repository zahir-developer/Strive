using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblPurchaseOrder")]
public class PurchaseOrder
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int PurchaseOrderId { get; set; }

	[Column]
	public int? ProductId { get; set; }

	[Column]
	public int? VendorId { get; set; }

	[Column]
	public bool? IsAutoRequest { get; set; }

	[Column]
	public bool? IsMailSent { get; set; }

	[Column]
	public DateTimeOffset? OrderedDate { get; set; }

	[Column]
	public int? OrderedBy { get; set; }

	[Column]
	public string OrderDetails { get; set; }

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