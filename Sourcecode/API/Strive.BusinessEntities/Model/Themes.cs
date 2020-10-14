using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblThemes")]
public class Themes
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int ThemeId { get; set; }

	[Column]
	public string ThemeName { get; set; }

	[Column]
	public string FontFace { get; set; }

	[Column]
	public string PrimaryColor { get; set; }

	[Column]
	public string SecondaryColor { get; set; }
    [Column]
    public string TertiaryColor { get; set; }

    [Column]
    public string NavigationColor { get; set; }

    [Column]
    public string BodyColor { get; set; }

        [Column]
	public string DefaultLogoPath { get; set; }

	[Column]
	public string DefaultTitle { get; set; }

	[Column]
	public string Comments { get; set; }

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