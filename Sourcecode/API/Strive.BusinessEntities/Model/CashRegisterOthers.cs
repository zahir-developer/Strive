using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblCashRegisterOthers")]
public class CashRegisterOthers
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int CashRegOtherId { get; set; }

	[Column]
	public decimal? CreditCard1 { get; set; }

	[Column]
	public decimal? CreditCard2 { get; set; }

	[Column]
	public decimal? CreditCard3 { get; set; }

	[Column]
	public decimal? Checks { get; set; }

	[Column]
	public decimal? Payouts { get; set; }

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