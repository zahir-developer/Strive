
CREATE PROCEDURE [CON].[uspGetVehicleById]
(@VehicleId int =null)
AS
BEGIN

SELECT
	cvl.VehicleId AS ClientVehicleId
	,cvl.ClientId
	,cvl.VehicleNumber
	,cvl.VehicleMfr AS VehicleMakeId
	,cvMfr.valuedesc AS VehicleMake
	,cvl.VehicleModel AS VehicleModelId
	,cvmo.valuedesc AS ModelName
	,cvCo.valuedesc AS Color
	,cvl.VehicleColor AS ColorId
	,cvl.Upcharge
	,cvl.Barcode
FROM strivecarsalon.tblClientVehicle cvl 
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid

WHERE ISNULL(cvl.IsDeleted,0)=0
AND
 (@VehicleId is null or cvl.VehicleId = @VehicleId)

END
