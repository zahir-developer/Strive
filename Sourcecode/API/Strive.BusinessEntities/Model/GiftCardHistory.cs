using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblGiftCardHistory")]
public class GiftCardHistory
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int GiftCardHistoryId { get; set; }

	[Column]
	public int? GiftCardId { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public int? TransactionType { get; set; }

	[Column]
	public decimal? TransactionAmount { get; set; }

	[Column]
	public int? TransactionUserId { get; set; }

	[Column]
	public DateTimeOffset? TransactionDate { get; set; }

	[Column]
	public string Comments { get; set; }

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