using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblChatUserGroup")]
public class ChatUserGroup
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int ChatGroupUserId { get; set; }

	[Column, PrimaryKey]
	public int? UserId { get; set; }

	[Column, PrimaryKey]
	public int? ChatGroupId { get; set; }

	[Column]
	public bool? IsActive { get; set; }

	[Column]
	public bool? IsDeleted { get; set; }

	[Column]
	public int? CreatedBy { get; set; }

	[Column]
	public DateTimeOffset? CreatedDate { get; set; }

}
}