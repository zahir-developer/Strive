using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblWhiteLabel")]
public class WhiteLabel
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int WhiteLabelId { get; set; }

	[Column]
	public string LogoPath { get; set; }
    [Ignore]
    public string FileName { get; set; }

    [Ignore]
    public string ThumbFileName { get; set; }

    [Ignore]
    public string Base64 { get; set; }

        [Column]
	public string Title { get; set; }

	[Column]
	public int? ThemeId { get; set; }

	[Column]
	public string FontFace { get; set; }

	[Column]
	public string PrimaryColor { get; set; }

	[Column]
	public string SecondaryColor { get; set; }

	[Column]
	public string TertiaryColor { get; set; }

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