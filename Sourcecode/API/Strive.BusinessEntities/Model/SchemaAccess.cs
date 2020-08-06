using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblSchemaAccess")]
public class SchemaAccess
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int SchemaAccessId { get; set; }

	[Column]
	public int AuthId { get; set; }

	[Column]
	public int SchemaId { get; set; }

	[Column]
	public short IsDeleted { get; set; }

}
}