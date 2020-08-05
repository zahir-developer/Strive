using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblBonusSetup")]
public class BonusSetup
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int BonusSetupId { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public int? BonusMonth { get; set; }

	[Column]
	public int? BonusYear { get; set; }

	[Column]
	public string BonusSlabName { get; set; }

	[Column]
	public int? MininumRange { get; set; }

	[Column]
	public int? MaximumRange { get; set; }

	[Column]
	public decimal? BonusAmount { get; set; }

	[Column]
	public int? StartsWithPaymentCycle { get; set; }

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