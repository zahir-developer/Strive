using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblTenantFeature")]
public class TenantFeature
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int FeatureId { get; set; }

	[Column]
	public int? ModuleId { get; set; }

	[Column]
	public string FeatureName { get; set; }

	[Column]
	public int? ParentFeatureId { get; set; }

	[Column]
	public string Comments { get; set; }

	[Column]
	public int? IsActive { get; set; }

	[Column]
	public int? IsDeleted { get; set; }

	[Column]
	public DateTime? CreatedDate { get; set; }

}
}