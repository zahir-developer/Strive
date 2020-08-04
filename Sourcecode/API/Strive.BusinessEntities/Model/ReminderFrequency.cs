using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblReminderFrequency")]
public class ReminderFrequency
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int ReminderFrequencyId { get; set; }

	[Column]
	public string Title { get; set; }

	[Column]
	public int? Frequency { get; set; }

	[Column]
	public bool? IsActive { get; set; }

	[Column]
	public bool? IsDeleted { get; set; }

}
}