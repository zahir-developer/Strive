using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblTenantConfig")]
public class TenantConfig
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int ConfigId { get; set; }

	[Column, PrimaryKey]
	public int TenantId { get; set; }

	[Column, PrimaryKey]
	public int ModuleId { get; set; }

	[Column, PrimaryKey]
	public int FeatureId { get; set; }

	[Column, PrimaryKey]
	public short IsDeleted { get; set; }

}
}