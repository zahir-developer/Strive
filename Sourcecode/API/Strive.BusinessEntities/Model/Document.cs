using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblDocument")]
public class Document
{

	[Column, IgnoreOnInsert, IgnoreOnUpdate]
	public int DocumentId { get; set; }

	[Column]
	public int? EmployeeId { get; set; }

	[Column]
	public string Filename { get; set; }

	[Column]
	public string Filepath { get; set; }

	[Column]
	public string Password { get; set; }

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