using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblCashRegister")]
public class CashRegister
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int CashRegisterId { get; set; }

	[Column]
	public int? CashRegisterType { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public int? DrawerId { get; set; }

	[Column]
	public int? UserId { get; set; }

	[Column]
	public int? CashRegisterCoinId { get; set; }

	[Column]
	public int? CashRegisterBillId { get; set; }

	[Column]
	public int? CashRegisterRollId { get; set; }

	[Column]
	public int? CashRegisterOtherId { get; set; }

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