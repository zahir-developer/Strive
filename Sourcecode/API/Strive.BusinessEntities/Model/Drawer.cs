using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblDrawer")]
public class Drawer
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int DrawerId { get; set; }

	[Column]
	public string DrawerName { get; set; }

	[Column]
	public int? LocationId { get; set; }

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