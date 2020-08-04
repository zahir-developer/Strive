using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblLastAuth")]
public class LastAuth
{

	[Column, PrimaryKey]
	public int AuthId { get; set; }

	[Column, PrimaryKey]
	public int ActionTypeId { get; set; }

	[Column]
	public DateTime? LastDate { get; set; }

}
}