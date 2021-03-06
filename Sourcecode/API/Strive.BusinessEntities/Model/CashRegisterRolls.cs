using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblCashRegisterRolls")]
public class CashRegisterRolls
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int CashRegRollId { get; set; }

	[Column, PrimaryKey]
	public int CashRegisterId { get; set; }

	[Column]
	public int? Pennies { get; set; }

	[Column]
	public int? Nickels { get; set; }

	[Column]
	public int? Dimes { get; set; }

	[Column]
	public int? Quarters { get; set; }

	[Column]
	public int? HalfDollars { get; set; }

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