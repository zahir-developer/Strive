using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblTenantMaster")]
public class TenantMaster
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int TenantId { get; set; }

	[Column]
	public Guid? TenantGuid { get; set; }

	[Column]
	public int? SubscriptionId { get; set; }

	[Column]
	public int? ClientId { get; set; }

	[Column]
	public short? IsActive { get; set; }

	[Column]
	public int? IsDeleted { get; set; }

	[Column]
	public short? EmpSize { get; set; }

	[Column]
	public short? UTCPlusMinus { get; set; }

	[Column]
	public short? TimeZoneMinus { get; set; }

	[Column]
	public DateTime? ExpiryDate { get; set; }

	[Column]
	public DateTime? CreatedDate { get; set; }

	[Column]
	public string SchemaId { get; set; }

}
}