using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblTenantIntegration")]
public class TenantIntegration
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int TenantIntegrationId { get; set; }

	[Column, PrimaryKey]
	public int TenantId { get; set; }

	[Column, PrimaryKey]
	public int ExtAppNameId { get; set; }

	[Column]
	public int ExtAppEnvironId { get; set; }

	[Column]
	public string ExtClientId { get; set; }

	[Column]
	public string ExtClientSecret { get; set; }

	[Column]
	public string ExtRedirectUrl { get; set; }

	[Column]
	public string ExtBaseUrl { get; set; }

	[Column]
	public string MinorVersion { get; set; }

	[Column]
	public int TimeOut { get; set; }

	[Column]
	public short RetryCount { get; set; }

	[Column, PrimaryKey]
	public short IsEnableLogs { get; set; }

	[Column, PrimaryKey]
	public short IsDeleted { get; set; }

	[Column, PrimaryKey]
	public long CreatedDate { get; set; }

}
}