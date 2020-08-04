using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblSchemaMaster")]
public class SchemaMaster
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int SchemaId { get; set; }

	[Column, PrimaryKey]
	public int ClientId { get; set; }

	[Column, PrimaryKey]
	public string SubDomain { get; set; }

	[Column]
	public string DBSchemaName { get; set; }

	[Column]
	public string DBUserName { get; set; }

	[Column]
	public string DBPassword { get; set; }

	[Column, PrimaryKey]
	public int SubscriptionId { get; set; }

	[Column]
	public int StatusId { get; set; }

	[Column, PrimaryKey]
	public short IsDeleted { get; set; }

	[Column]
	public DateTime? ExpiryDate { get; set; }

	[Column]
	public DateTime? CreatedDate { get; set; }

	[Column]
	public int? TenantId { get; set; }

}
}