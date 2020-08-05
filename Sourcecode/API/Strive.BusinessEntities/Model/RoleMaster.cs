using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblRoleMaster")]
public class RoleMaster
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int RoleMasterId { get; set; }

	[Column]
	public string RoleName { get; set; }

	[Column]
	public string RoleAlias { get; set; }

	[Column]
	public int? ParentId { get; set; }

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