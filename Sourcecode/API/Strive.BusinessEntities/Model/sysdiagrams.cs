using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("sysdiagrams")]
public class sysdiagrams
{

	[Column]
	public string name { get; set; }

	[Column]
	public int principal_id { get; set; }

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int diagram_id { get; set; }

	[Column]
	public int? version { get; set; }

	[Column]
	public byte[] definition { get; set; }

}
}