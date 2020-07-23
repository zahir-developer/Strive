using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblNotificationTemplate")]
public class NotificationTemplate
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int NotificationId { get; set; }

	[Column]
	public string NotificationName { get; set; }

	[Column]
	public int? NotificationType { get; set; }

	[Column]
	public string NotificationMessage { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public bool? IsInternal { get; set; }

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