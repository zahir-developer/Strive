using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblCashRegisterBills")]
public class CashRegisterBills
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int CashRegBillId { get; set; }

	[Column, PrimaryKey]
	public int CashRegisterId { get; set; }

	[Column]
	public int? s1 { get; set; }

	[Column]
	public int? s5 { get; set; }

	[Column]
	public int? s10 { get; set; }

	[Column]
	public int? s20 { get; set; }

	[Column]
	public int? s50 { get; set; }

	[Column]
	public int? s100 { get; set; }

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