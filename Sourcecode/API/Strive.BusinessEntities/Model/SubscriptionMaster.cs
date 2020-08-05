using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblSubscriptionMaster")]
public class SubscriptionMaster
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int SubscriptionId { get; set; }

	[Column, PrimaryKey]
	public string SubscriptionName { get; set; }

	[Column]
	public int StatusId { get; set; }

	[Column]
	public short IsDeleted { get; set; }

	[Column]
	public int CreatedEmpId { get; set; }

	[Column]
	public DateTime? ExpiryDate { get; set; }

	[Column]
	public DateTime? CreatedDate { get; set; }

}
}