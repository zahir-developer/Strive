using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblTenantModule")]
public class TenantModule
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int ModuleId { get; set; }

	[Column]
	public string ModuleName { get; set; }

	[Column]
	public int? ParentModuleId { get; set; }

	[Column]
	public string Comments { get; set; }

	[Column]
	public bool? IsActive { get; set; }

	[Column]
	public bool? IsDeleted { get; set; }

	[Column]
	public DateTime? CreatedDate { get; set; }

}
}