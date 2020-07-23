using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblCodeValue")]
public class CodeValue
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int id { get; set; }

	[Column]
	public int CategoryId { get; set; }

	[Column]
	public string codeValue { get; set; }

	[Column]
	public string Description { get; set; }

	[Column]
	public string CodeShortValue { get; set; }

	[Column]
	public int? ParentId { get; set; }

	[Column]
	public int? SortOrder { get; set; }

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