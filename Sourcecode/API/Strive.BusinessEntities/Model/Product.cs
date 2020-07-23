using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblProduct")]
public class Product
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int ProductId { get; set; }

	[Column]
	public string ProductName { get; set; }

	[Column]
	public string ProductCode { get; set; }

	[Column]
	public string ProductDescription { get; set; }

	[Column]
	public int? ProductType { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public int? VendorId { get; set; }

	[Column]
	public int? Size { get; set; }

	[Column]
	public string SizeDescription { get; set; }

	[Column]
	public Double? Quantity { get; set; }

	[Column]
	public string QuantityDescription { get; set; }

	[Column]
	public Double? Cost { get; set; }

	[Column]
	public bool? IsTaxable { get; set; }

	[Column]
	public Double? TaxAmount { get; set; }

	[Column]
	public int? ThresholdLimit { get; set; }

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