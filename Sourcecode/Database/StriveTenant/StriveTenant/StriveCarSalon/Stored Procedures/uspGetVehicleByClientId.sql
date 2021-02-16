CREATE PROC [StriveCarSalon].[uspGetVehicleByClientId] 
(@ClientId int =null)
AS
BEGIN

SELECT
	cvl.VehicleId AS ClientVehicleId
	,cvl.VehicleNumber
	,cvl.VehicleMfr AS VehicleMakeId
	,cvMfr.valuedesc AS VehicleMake
	,cvl.VehicleModel AS VehicleModelId
	,cvmo.valuedesc AS VehicleModelName
	,cvCo.valuedesc AS VehicleColor
	,cvl.VehicleColor AS VehicleColorId
	,cvl.Upcharge
	,cvl.Barcode
FROM
strivecarsalon.tblClientVehicle cvl
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid

WHERE ISNULL(cvl.IsDeleted,0)=0 AND ISNULL(cvl.IsActive,1)=1 AND
(@ClientId is null or cvl.ClientId = @ClientId)

END
