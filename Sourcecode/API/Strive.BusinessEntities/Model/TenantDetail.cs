using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblTenantDetail")]
public class TenantDetail
{

	[Column, PrimaryKey]
	public int TenantId { get; set; }

	[Column]
	public string TenantName { get; set; }

	[Column]
	public string ColorTheme { get; set; }

	[Column]
	public string Currency { get; set; }

	[Column]
	public string TelantLogoUrl { get; set; }

}
}