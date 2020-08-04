using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblResetAuth")]
public class ResetAuth
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int ResetAuthId { get; set; }

	[Column, PrimaryKey]
	public int AuthId { get; set; }

	[Column, PrimaryKey]
	public int ActionTypeId { get; set; }

	[Column, PrimaryKey]
	public string ResetHash { get; set; }

	[Column]
	public short IsDeleted { get; set; }

	[Column]
	public DateTime? ResetDate { get; set; }

}
}