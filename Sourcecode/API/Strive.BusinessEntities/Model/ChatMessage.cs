using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblChatMessage")]
public class ChatMessage
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public long ChatMessageId { get; set; }

	[Column]
	public string Subject { get; set; }

	[Column]
	public string Messagebody { get; set; }

	[Column]
	public long? ParentChatMessageId { get; set; }

	[Column]
	public DateTimeOffset? ExpiryDate { get; set; }

	[Column]
	public bool? IsReminder { get; set; }

	[Column]
	public DateTime? NextRemindDate { get; set; }

	[Column]
	public int? ReminderFrequencyId { get; set; }

	[Column]
	public int? CreatedBy { get; set; }

	[Column]
	public DateTimeOffset? CreatedDate { get; set; }

}
}