using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblWeatherPrediction")]
public class WeatherPrediction
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int WeatherId { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public string Weather { get; set; }

	[Column]
	public string RainProbability { get; set; }

	[Column]
	public string PredictedBusiness { get; set; }

	[Column]
	public string TargetBusiness { get; set; }

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