CREATE PROC [StriveCarSalon].[uspGetVehicle] 
(@ClientId int =null)
AS
BEGIN

SELECT
	cvl.VehicleId AS ClientVehicleId
	,cl.ClientId
	 ,cl.FirstName + ' '+cl.LastName as ClientName
	,cvl.VehicleNumber
	,cvl.VehicleMfr AS VehicleMakeId
	,cvMfr.valuedesc AS VehicleMake
	,cvl.VehicleModel AS VehicleModelId
	,cvmo.valuedesc AS ModelName
	,cvCo.valuedesc AS Color
	,cvl.VehicleColor AS ColorId
	,cvl.Upcharge
	,cvl.Barcode
FROM
strivecarsalon.tblclient cl
INNER JOIN strivecarsalon.tblClientVehicle cvl ON cl.ClientId = cvl.ClientId
INNER JOIN strivecarsalon.GetTable('VehicleManufacturer') cvMfr ON cvl.VehicleMfr = cvMfr.valueid
INNER JOIN strivecarsalon.GetTable('VehicleModel') cvMo ON cvl.VehicleModel = cvMo.valueid
INNER JOIN strivecarsalon.GetTable('VehicleColor') cvCo ON cvl.VehicleColor = cvCo.valueid

WHERE ISNULL(cl.IsDeleted,0)=0 AND ISNULL(cl.IsActive,1)=1 AND ISNULL(cvl.IsActive,1)=1 AND
ISNULL(cvl.IsDeleted,0)=0 AND
(@ClientId is null or cl.ClientId = @ClientId)

END