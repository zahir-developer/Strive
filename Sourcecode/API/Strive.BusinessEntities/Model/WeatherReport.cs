using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblWeatherReport")]
public class WeatherReport
{

	[Column]
	public int? locationId { get; set; }

	[Column]
	public decimal? Lan { get; set; }

	[Column]
	public decimal? Lon { get; set; }

	[Column]
	public int? Timestep { get; set; }

	[Column]
	public string UnitSystem { get; set; }

	[Column]
	public string Fields { get; set; }

	[Column]
	public DateTimeOffset? StartTime { get; set; }

	[Column]
	public DateTimeOffset? EndTime { get; set; }

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