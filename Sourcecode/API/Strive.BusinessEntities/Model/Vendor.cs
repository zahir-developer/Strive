using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblVendor")]
public class Vendor
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int VendorId { get; set; }

	[Column]
	public string VIN { get; set; }

	[Column]
	public string VendorName { get; set; }

	[Column]
	public string VendorAlias { get; set; }

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

	[Column]
	public string websiteAddress { get; set; }

	[Column]
	public string AccountNumber { get; set; }

}
}