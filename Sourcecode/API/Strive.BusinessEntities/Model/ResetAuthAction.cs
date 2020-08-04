using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblResetAuthAction")]
public class ResetAuthAction
{

	[Column, PrimaryKey]
	public int ResetAuthId { get; set; }

	[Column, PrimaryKey]
	public int AuthId { get; set; }

	[Column, PrimaryKey]
	public short ResetStatus { get; set; }

	[Column]
	public DateTime? ActionDate { get; set; }

}
}