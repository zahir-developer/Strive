using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
[OverrideName("tblClientVehicle")]
public class ClientVehicle
{

	[Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
	public int VehicleId { get; set; }

	[Column, PrimaryKey]
	public int? ClientId { get; set; }

	[Column]
	public int? LocationId { get; set; }

	[Column]
	public string VehicleNumber { get; set; }

	[Column]
	public int? VehicleMfr { get; set; }

	[Column]
	public int? VehicleModel { get; set; }

	[Column]
	public int? VehicleModelNo { get; set; }

	[Column]
	public string VehicleYear { get; set; }

	[Column]
	public int? VehicleColor { get; set; }

	[Column]
	public int? Upcharge { get; set; }

	[Column]
	public string Barcode { get; set; }

	[Column]
	public string Notes { get; set; }

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