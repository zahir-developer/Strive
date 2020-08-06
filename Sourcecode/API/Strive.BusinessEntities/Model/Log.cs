using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblLog")]
public class Log
{

	[Column]
	public int? LogId { get; set; }

	[Column]
	public string Logtext { get; set; }

	[Column]
	public DateTime? LogDate { get; set; }

	[Column]
	public string Schemaname { get; set; }

	[Column]
	public DateTimeOffset? CreatedDate { get; set; }

}
}